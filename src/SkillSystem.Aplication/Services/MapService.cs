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

    public static UserGetDto MapUserToUserDto(User user)
    {
        return new UserGetDto
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.UserRole,
        };
    }
    public static User MapUserCreateDtoToUser(UserCreateDto userCreateDto, string hashedPassword, string salt)
    {
        return new User
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PhoneNumber = userCreateDto.PhoneNumber,
            Password = hashedPassword,
            Salt = salt
        };
    }

}