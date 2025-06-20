using SkillSystem.Aplication.Dtos;
using SkillSystem.Domain.Entities;

namespace SkillSystem.Aplication.Services;

public class UserService : IUserService
{
    public Task<long> CreateAsync(UserCreateDto userCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserGetDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<UserGetDto>> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> GetByIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }
}
