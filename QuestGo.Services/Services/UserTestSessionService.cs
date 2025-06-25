using System.IO.MemoryMappedFiles;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

public class UserTestSessionService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IUserTestSessionService
{
    public async ValueTask<UserTestSessionResultDto> CreateAsync(UserTestSessionCreateDto dto)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == nameof(User.Id));
        var userId = long.Parse(userIdClaim!.Value);

        var mappedSession = mapper.Map<UserTestSession>(dto);
        mappedSession.UserId = userId;
        mappedSession.CreatedAt = DateTime.UtcNow.AddHours(5);

        var entity = await unitOfWork.UserTestSessions.InsertAsync(mappedSession);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<UserTestSessionResultDto>(entity);
    }

    public async ValueTask<PagedResultDto<UserTestSessionResultDto>> GetAllAsync(PaginationParams @params)
    {
        var sessions = unitOfWork.UserTestSessions.SelectAll(session => !session.IsDeleted).ToPagedList(@params);

        var count = await sessions.CountAsync();
        var mappedItems = mapper.Map<List<UserTestSessionResultDto>>(sessions);

        return new PagedResultDto<UserTestSessionResultDto>()
        {
            TotalCount = count,
            Items = mappedItems
        };
    }

    public async ValueTask<UserTestSessionResultDto> GetByIdAsync(long id)
    {
        var maybeSession = await unitOfWork.UserTestSessions.SelectAsync(
            session => session.Id == id);
        
        if(maybeSession == null || maybeSession.IsDeleted)
            throw new QuestGoException(404, "Test session not found");
        
        return mapper.Map<UserTestSessionResultDto>(maybeSession);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var maybeSession = await unitOfWork.UserTestSessions.SelectAsync(
            session => session.Id == id);
        
        if(maybeSession == null || maybeSession.IsDeleted)
            throw new QuestGoException(404, "Test session not found");
        
        maybeSession.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();
        
        return true;
    }
}