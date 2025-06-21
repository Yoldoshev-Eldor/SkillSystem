using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public interface IRoleService
{
    Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role);
    Task<List<UserRoleDto>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}