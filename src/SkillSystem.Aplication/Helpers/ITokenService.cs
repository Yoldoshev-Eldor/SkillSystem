using SkillSystem.Aplication.Dtos;
using System.Security.Claims;

namespace SkillSystem.Aplication.Helpers;

public interface ITokenService
{
    public string GenerateTokent(UserGetDto user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
