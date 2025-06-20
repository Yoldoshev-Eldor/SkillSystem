using SkillSystem.Aplication.Dtos;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Services;

public interface IUserService
{
    Task<long> CreateAsync(UserCreateDto userCreateDto);
    Task DeleteAsync(long userId);
    Task<UserGetDto> GetByIdAsync(long userId);
    Task<PagedResult<UserGetDto>> GetAllAsync(int skip, int take);
    ICollection<UserGetDto> GetAll();
    Task DeleteAsync(UserGetDto user);
}
