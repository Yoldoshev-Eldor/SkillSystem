using Microsoft.AspNetCore.Authorization;
using SkillSystem.Aplication.Services;

namespace SkillSystem.Api.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var mapGroup = app.MapGroup("/Admin")
           .RequireAuthorization()
           .WithTags("AdminManagement");

        mapGroup.MapGet("/get all users", [Authorize(Roles = "Admin,SuperAdmin")]
        async (int skip, int take, IUserService userService) =>
        {
            var users = await userService.GetAllAsync(skip, take);
            return users;
        })
            .WithName("GetAllUsers")
            .Produces(200)
            .Produces(400);

        mapGroup.MapDelete("/delete user/{userId}", [Authorize(Roles = "Admin,SuperAdmin")]
        async (long userId, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(userId);
            await userService.DeleteAsync(user.UserId);
        })
            .WithName("DeleteUser")
            .Produces(200)
            .Produces(404);

        mapGroup.MapGet("/get user/{userId}", [Authorize(Roles = "Admin,SuperAdmin")]
        async (long userId, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(userId);
            return user;
        })
            .WithName("GetUserById")
            .Produces(200)
            .Produces(404);
        mapGroup.MapGet("/get user/{userName}", [Authorize(Roles = "Admin,SuperAdmin")]
        async (string userName, IUserService userService) =>
        {
            var user = await userService.GetByUserNameAsync(userName);
            return user;
        })
            .WithName("GetUserByUsername")
            .Produces(200)
            .Produces(404);

        //mapGroup.MapPatch("/updateRole", [Authorize(Roles = "SuperAdmin")]
        //async (long userId, string userRole, IUserService userService) =>
        //{
        //    await userService.UpdateUserRoleAsync(userId, userRole);

        //})
        //    .WithName("UpdateUserRole")
        //    .Produces(200)
        //    .Produces(404)
        //    .Produces(403);

    }
}

