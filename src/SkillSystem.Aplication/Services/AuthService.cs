using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Helpers;
using SkillSystem.Aplication.Helpers.Security;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;
using System.Security.Claims;

namespace SkillSystem.Aplication.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository UserRepository;
    private readonly IRefreshTokenRepository RefreshTokenRepository;
    private readonly ITokenService TokenService;



    public AuthService(ITokenService tokenService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        TokenService = tokenService;
        UserRepository = userRepository;
        RefreshTokenRepository = refreshTokenRepository;
    }

    public async Task<LogInResponseDto> LoginUserAsync(UserCreateDto userLoginDto)
    {
        var user = await UserRepository.SelectByUserNameAsync(userLoginDto.UserName);

        var checkUserPassword = PasswordHasher.Verify(userLoginDto.Password, user.Password, user.Salt);

        if (!checkUserPassword)
        {
            throw new UnauthorizedException("UserName or password incorrect");
        }

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = (Role)RoleDto.User,
        };

        var token = TokenService.GenerateTokent(userGetDto);
        var refreshToken = TokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await RefreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        var loginResponseDto = new LogInResponseDto()
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            Expires = 24
        };


        return loginResponseDto;
    }

    public async Task LogOutAsync(string token)
    {
        await RefreshTokenRepository.RemoveRefreshTokenAsync(token);
    }

    public async Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        ClaimsPrincipal? principal = TokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null) throw new ForbiddenException("Invalid access token.");


        var userClaim = principal.FindFirst(c => c.Type == "UserId");
        var userId = long.Parse(userClaim.Value);


        var refreshToken = await RefreshTokenRepository.SelectRefreshTokenAsync(request.RefreshToken, userId);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.IsRevoked)
            throw new UnauthorizedException("Invalid or expired refresh token.");

        refreshToken.IsRevoked = true;

        var user = await UserRepository.SelectByIdAsync(userId);

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = (Role)RoleDto.User
        };

        var newAccessToken = TokenService.GenerateTokent(userGetDto);
        var newRefreshToken = TokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await RefreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LogInResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            TokenType = "Bearer",
            Expires = 900
        };
    }

    public async Task<long> SignUpUserAsync(UserCreateDto userCreateDto)
    {
        var tupleFromHasher = PasswordHasher.Hasher(userCreateDto.Password);
        var user = new User()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PhoneNumber = userCreateDto.PhoneNumber,
            Password = tupleFromHasher.Hash,
            Salt = tupleFromHasher.Salt,
            Role = (Role)RoleDto.User,
        };

        return await UserRepository.InsertAsync(user);
    }
}
