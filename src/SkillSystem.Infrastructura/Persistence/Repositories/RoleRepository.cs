using Microsoft.EntityFrameworkCore;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;


namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly MainContext _context;
    public RoleRepository(MainContext context)
    {
        _context = context;
    }
    public async Task<List<UserRole>> GetAllRolesAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        return roles;
    }

    public async Task<ICollection<User>> GetAllUsersByRoleAsync(string role)
    {
        var foundRole = await _context.Roles.Include(u => u.Users).FirstOrDefaultAsync(u => u.RoleName == role);
        if (foundRole is null)
        {
            throw new EntityNotFoundException(role);
        }
        return foundRole.Users;

    }

    public async Task<long> GetRoleIdAsync(string role)
    {
        var res = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == role);
        if (role is null)
        {
            throw new EntityNotFoundException(role);
        }
        return res.RoleId;
    }
}
