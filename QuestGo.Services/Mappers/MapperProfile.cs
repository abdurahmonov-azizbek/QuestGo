using AutoMapper;
using QuestGo.Domain.Entities;
using QuestGo.Services.Dtos;

namespace QuestGo.Services.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // User
        CreateMap<User, UserResultDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();

        // Category
        CreateMap<Category, CategoryResultDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        // Test
        CreateMap<Test, TestResultDto>();
        CreateMap<TestCreateDto, Test>();
        CreateMap<TestUpdateDto, Test>();
        
        // Question
        CreateMap<Question, QuestionResultDto>();
        CreateMap<QuestionCreateDto, Question>();
        CreateMap<QuestionUpdateDto, Question>();
    }
}
