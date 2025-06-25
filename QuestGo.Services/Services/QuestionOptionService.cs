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

public class QuestionOptionService(IUnitOfWork unitOfWork, IMapper mapper) : IQuestionOptionService
{
    public async ValueTask<QuestionOptionResultDto> CreateAsync(QuestionOptionCreateDto dto)
    {
        var mappedQuestionOption = mapper.Map<QuestionOption>(dto);
        var entity = await unitOfWork.QuestionOptions.InsertAsync(mappedQuestionOption);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<QuestionOptionResultDto>(entity);
    }

    public async ValueTask<PagedResultDto<QuestionOptionResultDto>> GetAllAsync(PaginationParams @params)
    {
        var questionOptions = unitOfWork.QuestionOptions.SelectAll(option => !option.IsDeleted)
            .ToPagedList(@params);

        var count = await questionOptions.CountAsync();
        var mappedItems = mapper.Map<List<QuestionOptionResultDto>>(questionOptions);

        return new PagedResultDto<QuestionOptionResultDto>()
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<QuestionOptionResultDto> GetByIdAsync(long id)
    {
        var maybeQuestionOption = await unitOfWork.QuestionOptions.SelectAsync(
            questionOption => questionOption.Id == id);
        
        if(maybeQuestionOption is null || maybeQuestionOption.IsDeleted)
            throw new QuestGoException(404, "Question option not found");
        
        return mapper.Map<QuestionOptionResultDto>(maybeQuestionOption);
    }

    public async ValueTask<bool> UpdateAsync(long id, QuestionOptionUpdateDto dto)
    {
        var maybeQuestionOption = await unitOfWork.QuestionOptions.SelectAsync(
            questionOption => questionOption.Id == id);
        
        if(maybeQuestionOption is null || maybeQuestionOption.IsDeleted)
            throw new QuestGoException(404, "Question option not found");
        
        mapper.Map(dto, maybeQuestionOption);
        await unitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var maybeQuestionOption = await unitOfWork.QuestionOptions.SelectAsync(
            questionOption => questionOption.Id == id);
        
        if(maybeQuestionOption is null || maybeQuestionOption.IsDeleted)
            throw new QuestGoException(404, "Question option not found");

        maybeQuestionOption.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
        
        return true;
    }
}