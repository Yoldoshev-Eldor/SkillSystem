using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Interfaces;

public interface ISkillRepository
{
    Task<long> InsertAsync(Skill skill);
    Task DeleteByIdAsync(long skillId);
    Task<Skill?> SelectByIdAsync(long skillId);
    Task<ICollection<Skill>> SelectSkillsAsync(int skip, int take);
    Task<ICollection<Skill>> SelectAll(long userId);
    Task UpdateAsync(Skill skill);
}
