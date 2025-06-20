using SkillSystem.Aplication.Dtos;
using SkillSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Aplication.Interfaces;

public interface IUserRepository
{
    Task<long> InsertAsync(User user);
    Task DeleteAsync(long userId);
    Task UpdateUserRoleAsync(long userId, Role role);
    Task<User?> SelectByIdAsync(long userId);
    Task<User?> SelectByUserNameAsync(string userName);
    Task<ICollection<User>> SelectAllUserAsync(int skip, int take);
    Task<PagedResult<User>> SelectAllAsync(int skip, int take);
    IQueryable<User> SelectAll();
}
