using Microsoft.AspNetCore.Authorization;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Services;

namespace SkillSystem.Api.Endpoints;

public static class UserRoleEndpoints
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        var roleGroup = app.MapGroup("/roles")
            .RequireAuthorization()
            .WithTags("Roles");

        roleGroup.MapPost("/insert-role", [Authorize(Roles = "Admin,SuperAdmin")]
        async (UserRoleCreateDto roleDto, IRoleService roleService) =>
            {
                var roleId = await roleService.InsertRoleAsync(roleDto);
                return Results.Ok(roleId);
            })
            .WithName("InsertRole")
            .Produces(200)
            .Produces(400);

        roleGroup.MapGet("/get-all-roles", [Authorize(Roles = "Admin,SuperAdmin")]
        async (IRoleService roleService) =>
            {
                var roles = await roleService.GetAllRolesAsync();
                return Results.Ok(roles);
            })
            .WithName("GetAllRoles")
            .Produces(200)
            .Produces(400);

        roleGroup.MapGet("/get-all-users-by-role", [Authorize(Roles = "Admin,SuperAdmin")]
        async (string role, IRoleService roleService) =>
        {
            var users = await roleService.GetAllUsersByRoleAsync(role);
            return Results.Ok(users);
        })
            .WithName("GetAllUsersByRole")
            .Produces(200)
            .Produces(400);

        roleGroup.MapDelete("/delete", [Authorize(Roles = "Admin,SuperAdmin")]
        async (long roleId, IRoleService roleService) =>
        {
            await roleService.DeleteAsync(roleId);
            return Results.Ok($"Role with ID {roleId} deleted successfully.");
        })
            .WithName("DeleteRole")
            .Produces(200)
            .Produces(400);


    }
}
