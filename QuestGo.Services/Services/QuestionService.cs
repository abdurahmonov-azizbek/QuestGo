using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;
using QuestGo.Services.Extensions;
using QuestGo.Services.Interfaces;

namespace QuestGo.Services.Services;

public class QuestionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQuestionService
{
    public async ValueTask<QuestionResultDto> CreateAsync(QuestionCreateDto dto)
    {
        var mappedQuestion = mapper.Map<Question>(dto);
        var question = await unitOfWork.Questions.InsertAsync(mappedQuestion);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<QuestionResultDto>(question);
    }

    public async ValueTask<PagedResultDto<QuestionResultDto>> GetAllAsync(PaginationParams @params)
    {
        var questions = unitOfWork.Questions.SelectAll()
            .Where(q => !q.IsDeleted)
            .ToPagedList(@params);

        var count = await questions.CountAsync();
        var items = await questions.ToListAsync();
        var mappedItems = mapper.Map<List<QuestionResultDto>>(items);

        return new PagedResultDto<QuestionResultDto>()
        {
            Items = mappedItems,
            TotalCount = count
        };
    }

    public async ValueTask<QuestionResultDto> GetByIdAsync(long questionId)
    {
        var maybeQuestion = await unitOfWork.Questions.SelectAsync(q => q.Id == questionId);
        if (maybeQuestion is null || maybeQuestion.IsDeleted)
            throw new QuestGoException(404, "Question not found!");

        return mapper.Map<QuestionResultDto>(maybeQuestion);
    }

    public async ValueTask<IList<QuestionResultDto>> GetByTestId(long testId)
    {
        var questions = await unitOfWork.Questions.SelectAll(q => q.TestId == testId && !q.IsDeleted).ToListAsync();
        var mappedQuestions = mapper.Map<List<QuestionResultDto>>(questions);

        return mappedQuestions;
    }

    public async ValueTask<bool> UpdateAsync(long questionId, QuestionUpdateDto dto)
    {
        var entity = await unitOfWork.Questions.SelectAsync(q => q.Id == questionId);
        if (entity is null || entity.IsDeleted)
            throw new QuestGoException(404, "Question not found!");

        mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow.AddHours(5);

        return await unitOfWork.SaveChangesAsync();
    }

    public async ValueTask<bool> DeleteAsync(long questionId)
    {
        var entity = await unitOfWork.Questions.SelectAsync(q => q.Id == questionId);
        if (entity is null || entity.IsDeleted)
            throw new QuestGoException(404, "Question not found!");

        entity.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}