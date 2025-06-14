using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using QuestGo.Api.Configurations.Layers;
using QuestGo.Api.Extensions;
using QuestGo.Api.Middlewares;
using QuestGo.Data.Contexts;
using QuestGo.Data.Implementations;
using QuestGo.Data.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin();
            policyBuilder.AllowAnyMethod();
            policyBuilder.AllowAnyHeader();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureJwtAuth();
builder.Services.ConfigureSwagger();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.ConfigureServices();

var app = builder.Build();
app.InitAccessor();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapScalarApiReference();

app.Run();
