using SkillSystem.Aplication.Dtos;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Services;

public static class MapService
{
    public static Skill MapSkillCreateDtoToSkill(SkillCreateDto dto)
    {
        return new Skill
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = dto.UserId,
            Level = (SkillLevel)SkillLevelDto.NotSet,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public static SkillGetDto MapSkillToSkillGetDto(Skill skill)
    {
        return new SkillGetDto
        {
            SkillId = skill.SkillId,
            Name = skill.Name,
            Description = skill.Description,
            Level = (SkillLevelDto)skill.Level,
            CreatedAt = skill.CreatedAt,
            UpdatedAt = skill.UpdatedAt,
        };
    }

    public static Skill MapSkillUpdateDtoToSkill(SkillUpdateDto skillUpdateDto)
    {
        return new Skill
        {
            SkillId = skillUpdateDto.SkillId,
            Name = skillUpdateDto.Name,
            Description = skillUpdateDto.Description,
            Level = (SkillLevel)skillUpdateDto.Level,
            UpdatedAt = DateTime.UtcNow
        };
    }
}