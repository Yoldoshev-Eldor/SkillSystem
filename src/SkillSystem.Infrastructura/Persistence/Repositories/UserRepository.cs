using Microsoft.EntityFrameworkCore;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;

namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MainContext _mainContext;
    public UserRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<long> InsertAsync(User user)
    {
        await _mainContext.Users.AddAsync(user);
        await _mainContext.SaveChangesAsync();
        return user.UserId;
    }

    public async Task DeleteAsync(long userId)
    {
        var user = await SelectByIdAsync(userId);
        if (user != null)
        {
            _mainContext.Users.Remove(user);
            await _mainContext.SaveChangesAsync();
        }
    }

    public IQueryable<User> SelectAll() => _mainContext.Users.AsQueryable();

    public async Task<PagedResult<User>> SelectAllAsync(int skip, int take)
    {
        var totalCount = await _mainContext.Users.CountAsync();

        var users = await _mainContext.Users
            .OrderBy(u => u.UserId)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return new PagedResult<User>
        {
            TotalCount = totalCount,
            Data = users
        };

    }

    public async Task<User?> SelectByIdAsync(long userId)
    {
        var user = await _mainContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        return user ?? throw new EntityNotFoundException();
    }

    public async Task<User?> SelectByUserNameAsync(string userName)
    {
        var user = await _mainContext.Users.FirstOrDefaultAsync(u => u.UserName == userName) ?? throw new EntityNotFoundException();
        return user;
    }

    public async Task UpdateUserRoleAsync(long userId, UserRole role)
    {
        var user = await SelectByIdAsync(userId) ?? throw new EntityNotFoundException($"User with ID {userId} not found");
        user.URole = role;
        _mainContext.Update(user);
        await _mainContext.SaveChangesAsync();
    }

    public Task<ICollection<User>> SelectAllUserAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }
}
