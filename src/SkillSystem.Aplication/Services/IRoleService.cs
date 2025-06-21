using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public interface IRoleService
{
    Task<long> InsertRoleAsync(UserRoleCreateDto roleDto);
    Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role);
    Task<List<UserRoleDto>> GetAllRolesAsync();
    Task<UserRoleDto> GetRoleIdAsync(long roleId);
    Task DeleteAsync(long roleId);
}