﻿using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Services;
using SkillSystem.Domain.Errors;

namespace SkillSystem.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/auth")
            .WithTags("Auth endpoints");

        userGroup.MapPost("/signIn", SignIn)
            .WithName("LogIn");

        userGroup.MapPost("/sighUp", SignUp)
            .WithName("SignUp");

        userGroup.MapPost("/refreshToken", RefreshToken)
            .WithName("RefreshToken");

        userGroup.MapDelete("/LogOut", LogOut)
            .WithName("LogOut");

        userGroup.MapGet("/getUser", GetUser)
            .WithName("GetUser");

        //.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,SuperAdmin" })
    }

    public static async Task<IResult> GetUser(HttpContext context, IUserService userService)
    {
        var userId = context.User.FindFirst("UserId")?.Value;
        if (userId is null)
            throw new ForbiddenException("Access forbidden");
        return Results.Ok(await userService.GetByIdAsync(long.Parse(userId)));
    }

    public static async Task<IResult> SignUp(UserCreateDto user, IAuthService _service)
    {
        return Results.Ok(await _service.SignUpAsync(user));
    }

    public static async Task<IResult> SignIn(UserLogInDto userLogInDto, IAuthService _service)
    {
        return Results.Ok(await _service.LogInAsync(userLogInDto));
    }

    public static async Task<IResult> RefreshToken(RefreshRequestDto refresh, IAuthService _service)
    {
        return Results.Ok(await _service.RefreshTokenAsync(refresh));
    }

    public static async Task<IResult> LogOut(string token, IAuthService _service)
    {
        await _service.LogOutAsync(token);
        return Results.Ok();
    }
}
