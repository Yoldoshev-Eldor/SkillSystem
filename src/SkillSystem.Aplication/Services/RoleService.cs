using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
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
    public async Task<long> InsertRoleAsync(UserRoleCreateDto roleDto)
    {
        var role = new UserRole
        {
            RoleName = roleDto.RoleName,
            Description = roleDto.Description
        };
        return await _roleRepository.InsertRole(role);
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

    public async Task<UserRoleDto> GetRoleIdAsync(long roleId)
    {
        var res = _roleRepository.GetRoleIdAsync(roleId);
        if (res is null)
        {
            throw new EntityNotFoundException();
        }
        var roleDto=new UserRoleDto
        {
            RoleId = res.Result.RoleId,
            RoleName = res.Result.RoleName,
            Description = res.Result.Description
        };
        return roleDto;

    }
    public async Task DeleteAsync(long roleId)
    {
        var role = await _roleRepository.GetRoleIdAsync(roleId);
        if (role is null)
        {
            throw new EntityNotFoundException($"Role with ID {roleId} not found.");
        }
        await _roleRepository.DeleteAsync(roleId);
    }
}
