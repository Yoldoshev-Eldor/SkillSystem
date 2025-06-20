using Microsoft.EntityFrameworkCore;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;

namespace SkillSystem.Infrastructura.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MainContext MainContext;

    public RefreshTokenRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task InsertRefreshTokenAsync(RefreshToken refreshToken)
    {
        await MainContext.RefreshTokens.AddAsync(refreshToken);
        await MainContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> SelectActiveTokenByUserIdAsync(long userId)
    {
        RefreshToken? refreshToke;
        try
        {
            refreshToke = await MainContext.RefreshTokens
            .Include(rf => rf.User)
            .SingleOrDefaultAsync(rf => rf.UserId == userId && !rf.IsRevoked && rf.Expires > DateTime.UtcNow);
        }
        catch (InvalidOperationException ex)
        {
            throw new DuplicateEntryException($"2 or more active refreshToken found with userId: {userId} found!\nAnd {ex.Message}");
        }
        return refreshToke;
    }

    public async Task<RefreshToken> SelectRefreshTokenAsync(string refreshToken, long userId)
    {
        var refToken = await MainContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
        return refToken ?? throw new InvalidArgumentException($"RefreshToken with {userId} is invalid");
    }
    public async Task RemoveRefreshTokenAsync(string token)
    {
        var refreshToken = await MainContext.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == token);
        if (refreshToken == null) throw new EntityNotFoundException($"Refresh token: {refreshToken} not found");

        MainContext.RefreshTokens.Remove(refreshToken);
        await MainContext.SaveChangesAsync();
    }
}
