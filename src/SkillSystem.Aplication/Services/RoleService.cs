using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Aplication.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<List<UserRoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return roles.Select(role => new UserRoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        }).ToList();
    }

    public async Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role)
    {
        var users = await _roleRepository.GetAllUsersByRoleAsync(role);

        if (users is null || users.Count == 0)
        {
            throw new EntityNotFoundException($"No users found for role: {role}");
        }
        var userDtos = users.Select(u => MapService.MapUserToUserDto(u)).ToList();

        return userDtos;
    }

    public Task<long> GetRoleIdAsync(string role)
    {
        var res = _roleRepository.GetRoleIdAsync(role);
        if (res is null)
        {
            throw new EntityNotFoundException(role);
        }
        return res;
    }
}
