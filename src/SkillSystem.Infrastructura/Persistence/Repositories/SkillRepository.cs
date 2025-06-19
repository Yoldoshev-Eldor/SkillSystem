using Microsoft.EntityFrameworkCore;
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

    public async Task DeleteByIdAsync(long skillId)
    {
        var skill = await SelectByIdAsync(skillId);
        mainContext.Skills.Remove(skill);
        await mainContext.SaveChangesAsync();
    }

    public async Task<long> InsertAsync(Skill skill)
    {
        await mainContext.Skills.AddAsync(skill);
        await mainContext.SaveChangesAsync();
        return skill.SkillId;
    }

    public async Task<ICollection<Skill>> SelectAll(long userId)
    {
        return await mainContext.Skills.Where(s => s.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Skill> SelectByIdAsync(long skillId)
    {
        var skill = await mainContext.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId);
        if (skill == null)
        {
            throw new KeyNotFoundException($"Skill with ID {skillId} not found.");
        }
        return skill;
    }

    public async Task UpdateAsync(Skill skill)
    {
        await SelectByIdAsync(skill.SkillId);
        mainContext.Skills.Update(skill);
        await mainContext.SaveChangesAsync();
    }
}
