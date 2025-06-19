using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task DeleteAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertAsync(User user)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> SelectAll()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> SelectAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task<User?> SelectByIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<User?> SelectByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserRoleAsync(long userId, string role)
    {
        throw new NotImplementedException();
    }
}
