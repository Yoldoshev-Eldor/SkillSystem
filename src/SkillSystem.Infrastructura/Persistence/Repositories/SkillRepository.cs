using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly MainContext mainContext;

    public SkillRepository(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public Task DeleteByIdAsync(long skillId)
    {
        throw new NotImplementedException();
    }

    public async Task<long> InsertAsync(Skill skill)
    {
        await mainContext.Skills.AddAsync(skill);
        await mainContext.SaveChangesAsync();
        return skill.SkillId;
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
