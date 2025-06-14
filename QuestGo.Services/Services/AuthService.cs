using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Entities;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Dtos;
using QuestGo.Services.Helpers;
using QuestGo.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuestGo.Services.Services;

public class AuthService(
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : IAuthService
{
    public async ValueTask<LoginResultDto> AuthenticateAsync(LoginDto dto)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Email == dto.Email && !u.IsDeleted);
        if (user is null)
            throw new QuestGoException(404, "User not found!");

        if (!PasswordHelper.Verify(dto.Password, user.PasswordHash))
            throw new QuestGoException(401, "Password is invalid!");

        return new LoginResultDto
        {
            Token = GenerateToken(user)
        };
    }

    public async ValueTask<UserResultDto> GetCurrentUserAsync()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext?.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
            ?? throw new QuestGoException(404, "User id claim not found");

        var userId = long.Parse(userIdClaim);

        var user = await unitOfWork.Users.SelectAsync(u => u.Id == userId && !u.IsDeleted)
            ?? throw new QuestGoException(404, "User not found.");

        return mapper.Map<UserResultDto>(user);
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString())
            }),
            Audience = configuration["JWT:Audience"],
            Issuer = configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.Now.AddHours(int.Parse(configuration["JWT:Lifetime"]!)),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
