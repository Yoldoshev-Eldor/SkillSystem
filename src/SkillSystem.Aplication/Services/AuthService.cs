using FluentValidation;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Helpers.Security;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;
using System.Security.Claims;

namespace SkillSystem.Aplication.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IValidator<UserLogInDto> _userLoginValidator;
    private readonly IValidator<UserCreateDto> _userCreateValidators;
    private readonly IRoleRepository _roleRepository;


    public AuthService(IRoleRepository roleRepository,IValidator<UserCreateDto> userCreateValidator, IValidator<UserLogInDto> validator, ITokenService tokenService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _userLoginValidator = validator;
        _userCreateValidators = userCreateValidator;
        _roleRepository = roleRepository;

    }
    public async Task<long> SignUpAsync(UserCreateDto userCreateDto)
    {
        var validationResult = await _userCreateValidators.ValidateAsync(userCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
        };
        user.RoleId = await _roleRepository.GetRoleIdAsync("User");
        return await _userRepository.InsertAsync(user);
    }
    public async Task<LogInResponseDto> LogInAsync(UserLogInDto userLoginDto)
    {
        var user = await _userRepository.SelectByUserNameAsync(userLoginDto.UserName);
        var checkpassword = PasswordHasher.Verify(userLoginDto.Password, user.Password, user.Salt);
        if (user is null || !checkpassword)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        var validationResult = await _userLoginValidator.ValidateAsync(userLoginDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var userDto = MapService.MapUserToUserDto(user);
        var accessToken = _tokenService.GenerateToken(userDto);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenToDB = new RefreshToken()
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };
        await _refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LogInResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            Expires = 24,
        };

    }


    public async Task LogOutAsync(string token)
    {
        await _refreshTokenRepository.RemoveRefreshTokenAsync(token);
    }

    public async Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null) throw new ForbiddenException("Invalid access token.");


        var userClaim = principal.FindFirst(c => c.Type == "UserId");
        var userId = long.Parse(userClaim.Value);


        var refreshToken = await _refreshTokenRepository.SelectRefreshTokenAsync(request.RefreshToken, userId);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.IsRevoked)
            throw new UnauthorizedException("Invalid or expired refresh token.");

        refreshToken.IsRevoked = true;

        var user = await _userRepository.SelectByIdAsync(userId);

        var userGetDto = MapService.MapUserToUserDto(user);


        var newAccessToken = _tokenService.GenerateToken(userGetDto);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await _refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LogInResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            TokenType = "Bearer",
            Expires = 24
        };
    }
}
