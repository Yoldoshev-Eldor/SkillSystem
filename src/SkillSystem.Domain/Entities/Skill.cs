namespace SkillSystem.Domain.Entities;

public class Skill
{
    public long SkillId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SkillLevel Level { get; set; } = SkillLevel.NotSet;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public long UserId { get; set; }
    public User? User { get; set; }
}
