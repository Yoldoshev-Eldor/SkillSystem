namespace SkillSystem.Domain.Repositories;

using Microsoft.Azure.Search.Models;
using SkillSystem.Domain.Entities;

public interface ISkillRepository
{
    Task AddAsync(Skill skill, CancellationToken cancellationToken);
}