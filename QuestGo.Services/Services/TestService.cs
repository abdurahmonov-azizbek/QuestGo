using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;
using QuestGo.Services.Extensions;
using QuestGo.Services.Interfaces;

namespace QuestGo.Services.Services;

public class TestService(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : ITestService
{
    public async ValueTask<TestResultDto> CreateAsync(TestCreateDto dto)
    {
        var existingTest = await unitOfWork.Tests.SelectAsync(t => t.Name == dto.Name);

        if (existingTest is not null && !existingTest.IsDeleted)
        {
            throw new QuestGoException(409, "Test already exist!");
        }

        var httpContext = httpContextAccessor.HttpContext;
        var userId = long.Parse(httpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!);

        var mappedTest = mapper.Map<Test>(dto);
        mappedTest.UserId = userId;
        var test = await unitOfWork.Tests.InsertAsync(mappedTest);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<TestResultDto>(test);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var test = await unitOfWork.Tests.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new QuestGoException(404, "Test not found!");

        test.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<PagedResultDto<TestResultDto>> GetAllAsync(PaginationParams @params)
    {
        var tests = unitOfWork.Tests.SelectAll()
            .Where(t => !t.IsDeleted)
            .ToPagedList(@params);

        var count = await tests.CountAsync();
        var items = await tests.ToListAsync();
        var mappedItems = mapper.Map<List<TestResultDto>>(items);

        return new PagedResultDto<TestResultDto>
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<PagedResultDto<TestResultDto>> GetAllByUserIdAsync(long userId, PaginationParams @params)
    {
        var tests = unitOfWork.Tests.SelectAll()
            .Where(t => t.UserId == userId && !t.IsDeleted)
            .ToPagedList(@params);

        var count = await tests.CountAsync();
        var items = await tests.ToListAsync();
        var mappedItems = mapper.Map<List<TestResultDto>>(items);

        return new PagedResultDto<TestResultDto>
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<TestResultDto> GetByIdAsync(long id)
    {
        var test = await unitOfWork.Tests.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new QuestGoException(404, "Test not found!");

        return mapper.Map<TestResultDto>(test);
    }

    public async ValueTask<bool> UpdateAsync(long testId, TestUpdateDto dto)
    {
        var test = await unitOfWork.Tests.SelectAsync(t => t.Id == testId && !t.IsDeleted)
            ?? throw new QuestGoException(404, "Test not found!");

        mapper.Map(dto, test);
        test.UpdatedAt = DateTime.UtcNow.AddHours(5);

        return await unitOfWork.Tests.SaveChangesAsync();
    }
}
