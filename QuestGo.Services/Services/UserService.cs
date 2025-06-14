using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Domain.Enums;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;
using QuestGo.Services.Extensions;
using QuestGo.Services.Helpers;
using QuestGo.Services.Interfaces;

namespace QuestGo.Services.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async ValueTask<UserResultDto> CreateAsync(UserCreateDto dto)
    {
        var existingUser = await unitOfWork.Users.SelectAsync(u => u.Email == dto.Email);
        if (existingUser is not null && !existingUser.IsDeleted)
            throw new QuestGoException(409, "User already exists!");

        var mappedUser = mapper.Map<User>(dto);
        mappedUser.Role = Role.User;
        mappedUser.PasswordHash = PasswordHelper.Hash(dto.Password);

        var user = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id); 

        if (user is null || user.IsDeleted)
            throw new QuestGoException(404, "User not found!");

        user.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<PagedResultDto<UserResultDto>> GetAllAsync(PaginationParams @params)
    {
        var users = unitOfWork.Users.SelectAll()
            .Where(u => !u.IsDeleted)
            .ToPagedList(@params);

        var count = await users.CountAsync();
        var items = await users.ToListAsync();
        var mappedItems = mapper.Map<List<UserResultDto>>(items);

        return new PagedResultDto<UserResultDto>()
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<UserResultDto> GetByEmailAsync(string email)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Email == email && !u.IsDeleted);

        if (user is null)
            throw new QuestGoException(404, "User not found!");

        return mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<UserResultDto> GetByIdAsync(long id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted);

        if (user is null)
            throw new QuestGoException(404, "User not found!");

        return mapper.Map<UserResultDto>(user);
    }

    public async ValueTask<bool> UpdateAsync(long id, UserUpdateDto dto)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id);
        if (user is null)
            throw new QuestGoException(404, "User not found");

        mapper.Map(dto, user);
        user.UpdatedAt = DateTime.Now;

        return await unitOfWork.Users.SaveChangesAsync();
    }
}
