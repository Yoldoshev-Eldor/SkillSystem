using SkillSystem.Aplication.Dtos;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Interfaces;

public interface IRoleRepository
{
    Task<long>InsertRole(UserRole role);
    Task<ICollection<User>> GetAllUsersByRoleAsync(string role);
    Task<List<UserRole>> GetAllRolesAsync();
    Task<UserRole> GetRoleIdAsync(long roleId);
    Task<long> GetRoleByNameAsync(string roleName);
    Task DeleteAsync(long roleId);

}
