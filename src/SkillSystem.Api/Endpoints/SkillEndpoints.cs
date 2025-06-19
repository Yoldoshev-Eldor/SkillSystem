using Microsoft.AspNetCore.Builder;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Services;

namespace SkillSystem.Api.Endpoints;

public static class SkillEndpoints
{
    public static void MapSkillEndpoints(this WebApplication app)
    {
        var skillGroup = app.MapGroup("Skills")
            //.RequireAuthorization()
            .WithTags("SkillManagement");
        skillGroup.MapPost("/post",
            async (SkillCreateDto skillDto, ISkillService skillService) =>
            {
                var skillId = await skillService.PostAsync(skillDto);
                return skillId;

            })
            .WithName("CreateSkill")
            .Produces(200)
            .Produces(404);

        skillGroup.MapDelete("/delete/{skillId:long}",
            async (long skillId, ISkillService skilService) =>
            {
                await skilService.DeleteAsync(skillId);
            })
            .WithName("DeleteSkill")
            .Produces(200)
            .Produces(404);

        skillGroup.MapGet("/get/{skillId:long}",
            async (long skillId, ISkillService skillService) =>
            {
                var skill = await skillService.GetByIdAsync(skillId);
                return skill;
            })
            .WithName("GetSkillById")
            .Produces<SkillGetDto>(200)
            .Produces(404);

        skillGroup.MapGet("/getAll",
            (ISkillService skillService) =>
            {
                var skills = skillService.GetAll();
                return skills;
            })
            .WithName("GetAllSkills")
            .Produces(200)
            .Produces(404);

        skillGroup.MapGet("/getSkills/{skip:int}/{take:int}",
            async (int skip, int take, ISkillService skillService) =>
            {
                var skills = await skillService.GetSkillsAsync(skip, take);
                return skills;
            })
            .WithName("GetSkills")
            .Produces(200)
            .Produces(404);

        skillGroup.MapPut("/update",
            async (SkillUpdateDto skillUpdateDto, ISkillService skillService) =>
            {
                await skillService.UpdateAsync(skillUpdateDto);
            })
            .WithName("UpdateSkill")
            .Produces(200)
            .Produces(404);
    }
}
