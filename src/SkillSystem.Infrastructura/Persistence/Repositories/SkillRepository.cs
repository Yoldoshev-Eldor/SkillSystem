using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    public Task DeleteByIdAsync(long skillId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertAsync(Skill skill)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Skill>> SelectAll()
    {
        throw new NotImplementedException();
    }

    public Task<Skill> SelectByIdAsync(long skillId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Skill skill)
    {
        throw new NotImplementedException();
    }
}
