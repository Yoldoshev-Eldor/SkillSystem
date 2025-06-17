using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Interfaces;

public interface ISkillRepository
{
    Task<long> InsertAsync(Skill skill);
    Task DeleteByIdAsync(long skillId);
    Task<Skill> SelectByIdAsync(long skillId);
    Task<ICollection<Skill>> SelectAll();
    Task UpdateAsync(Skill skill);
}
