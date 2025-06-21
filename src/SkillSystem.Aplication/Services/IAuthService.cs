using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public interface IAuthService
{
    Task<long> SignUpAsync(UserCreateDto userCreateDto);
    Task<LogInResponseDto> LogInAsync(UserLogInDto userLoginDto);
    Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request);
    Task LogOutAsync(string token);
}