using Microsoft.Azure.Search.Models;

namespace SkillSystem.Domain.Entities
{
    public SkillRepository(SkillSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Skill skill, CancellationToken cancellationToken)
    {
        await _context.Skills.AddAsync(skill, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
