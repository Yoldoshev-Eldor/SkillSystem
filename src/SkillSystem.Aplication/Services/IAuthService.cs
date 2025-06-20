using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public interface IAuthService
{
    Task<long> SignUpUserAsync(UserCreateDto userCreateDto);
    Task<LogInResponseDto> LoginUserAsync(UserCreateDto userLoginDto);
    Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request);
    Task LogOutAsync(string token);
}