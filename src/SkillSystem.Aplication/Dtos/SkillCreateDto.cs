namespace SkillSystem.Aplication.Dtos;

public class SkillCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SkillLevelDto Level { get; set; } = SkillLevelDto.NotSet;
    public long UserId { get; set; }
}
