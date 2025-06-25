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

public class UserAnswerService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserAnswerService
{
    public async ValueTask<UserAnswerResultDto> CreateAsync(UserAnswerCreateDto dto)
    {
        var mappedUserAnswer = mapper.Map<UserAnswer>(dto);
        var entity = await unitOfWork.UserAnswers.InsertAsync(mappedUserAnswer);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<UserAnswerResultDto>(entity);
    }

    public async ValueTask<PagedResultDto<UserAnswerResultDto>> GetAllAsync(PaginationParams @params)
    {
        var userAnswers = unitOfWork.UserAnswers.SelectAll(x => !x.IsDeleted).ToPagedList(@params);
        
        var count = await userAnswers.CountAsync();
        var mappedItems = mapper.Map<List<UserAnswerResultDto>>(userAnswers);

        return new PagedResultDto<UserAnswerResultDto>()
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<UserAnswerResultDto> GetByIdAsync(long id)
    {
        var maybeUserAnswer = await unitOfWork.UserAnswers.SelectAsync(x => x.Id == id);
        
        if (maybeUserAnswer == null || maybeUserAnswer.IsDeleted)
        {
            throw new QuestGoException(404, "UserAnswer not found");
        }
        
        return mapper.Map<UserAnswerResultDto>(maybeUserAnswer);
    }

    public async ValueTask<bool> UpdateAsync(long id, UserAnswerUpdateDto dto)
    {
        var maybeUserAnswer = await unitOfWork.UserAnswers.SelectAsync(x => x.Id == id);
        
        if (maybeUserAnswer == null || maybeUserAnswer.IsDeleted)
        {
            throw new QuestGoException(404, "UserAnswer not found");
        }
        
        mapper.Map(dto, maybeUserAnswer);
        maybeUserAnswer.UpdatedAt = DateTime.UtcNow.AddHours(5);
        await  unitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var maybeUserAnswer = await unitOfWork.UserAnswers.SelectAsync(x => x.Id == id);
        
        if (maybeUserAnswer == null || maybeUserAnswer.IsDeleted)
        {
            throw new QuestGoException(404, "UserAnswer not found");
        }
        
        maybeUserAnswer.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
        
        return true;
    }
}