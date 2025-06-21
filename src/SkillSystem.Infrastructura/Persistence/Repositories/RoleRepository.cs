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
    public async Task<long> InsertRole(UserRole role)
    {
        var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == role.RoleName);
        if (existingRole is not null)
        {
            throw new Exception($"Role with name {role.RoleName} already exists.");
        }
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role.RoleId;
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

    public async Task<UserRole> GetRoleIdAsync(long roleId)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        if (role is null)
        {
            throw new EntityNotFoundException();
        }

        return role;
    }
    public async Task DeleteAsync(long roleId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        if (role is null)
        {
            throw new EntityNotFoundException($"Role with ID {roleId} not found.");
        }
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    public async Task<long> GetRoleByNameAsync(string roleName)
    {
      var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        if (role is null)
        {
            throw new EntityNotFoundException($"Role with name {roleName} not found.");
        }
        return role.RoleId;
    }
}
