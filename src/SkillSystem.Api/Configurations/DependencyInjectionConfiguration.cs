﻿using FluentValidation;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.FluentValidation;
using SkillSystem.Aplication.Helpers;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Aplication.Services;
using SkillSystem.Application.FluentValidation;
using SkillSystem.Infrastructura.Persistence.Repositories;


namespace SkillSystem.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ISkillService, SkillService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IValidator<SkillCreateDto>, SkillCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<SkillUpdateDto>, SkillUpdateDtoValidator>();

            builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();

            builder.Services.AddScoped<IValidator<UserLogInDto>, UserLogInValidators>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();


        }
    }
}
