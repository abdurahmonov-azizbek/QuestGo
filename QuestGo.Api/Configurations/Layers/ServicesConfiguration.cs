using QuestGo.Services.Interfaces;
using QuestGo.Services.Mappers;
using QuestGo.Services.Services;

namespace QuestGo.Api.Configurations.Layers;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ITestService, TestService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddAutoMapper(typeof(MapperProfile));
    }
}