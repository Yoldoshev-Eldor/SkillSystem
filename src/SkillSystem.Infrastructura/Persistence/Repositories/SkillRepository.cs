using Microsoft.EntityFrameworkCore;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Core.Errors;
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
        var skill = await SelectByIdAsync(skillId) ?? throw new NotFoundException($"Skill with ID {skillId} not found.");
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

    public async Task<Skill?> SelectByIdAsync(long skillId)
    {
        var skill = await mainContext.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId) ?? throw new NotFoundException($"Skill with ID {skillId} not found.");
        return skill;
    }

    public async Task<ICollection<Skill>> SelectSkillsAsync(int skip, int take)
    {
        var skills = await mainContext.Skills
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return skills;
    }

    public async Task UpdateAsync(Skill skill)
    {
        await SelectByIdAsync(skill.SkillId);
        mainContext.Skills.Update(skill);
        await mainContext.SaveChangesAsync();
    }
}
