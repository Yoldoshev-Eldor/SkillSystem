using SkillSystem.Aplication.Dtos;
using System.Security.Claims;

namespace SkillSystem.Aplication.Helpers;

public interface ITokenService
{
    public string GenerateToken(UserGetDto userDto);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    public string RemoveRefreshTokenAsync(string token);
}
