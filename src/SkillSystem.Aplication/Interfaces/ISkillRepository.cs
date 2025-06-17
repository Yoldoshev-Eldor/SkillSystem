using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Interfaces;

public interface ISkillRepository
{
    Task<long> InsertAsync(Skill skill);
    Task DeleteByIdAsync(long skillId, long userId);
    Task<Skill> SelectByIdAsync(long skillId, long userId);
    Task<ICollection<Skill>> SelectAll();
    Task UpdateAsync(Skill skill);
}
