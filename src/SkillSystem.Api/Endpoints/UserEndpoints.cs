using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Services;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/users");
        group.MapGet("/", async (IUserService userService) =>
        {
            return await userService.GetAllAsync(0, 10);
        })
        .WithName("GetAllUsers")
        .Produces<PagedResult<User>>(StatusCodes.Status200OK);
        group.MapGet("/{id:long}", async (long id, IUserService userService) =>
        {
            return await userService.GetByIdAsync(id);
        })
        .WithName("GetUserById")
        .Produces<UserGetDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        group.MapPost("/", async (UserCreateDto userCreateDto, IUserService userService) =>
        {
            var userId = await userService.CreateAsync(userCreateDto);
            return Results.Created($"/api/users/{userId}", new { UserId = userId });
        })
        .WithName("CreateUser")
        .Produces<long>(StatusCodes.Status201Created);
       
        group.MapDelete("/{id:long}", async (long id, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }
            await userService.DeleteAsync(user);
            return Results.NoContent();
        })
        .WithName("DeleteUser")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
