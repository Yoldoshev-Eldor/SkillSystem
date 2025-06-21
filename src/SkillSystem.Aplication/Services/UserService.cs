using FluentValidation;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Helpers.Security;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;

namespace SkillSystem.Aplication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateDto> _userCreateDtoValidator;

    public UserService(IUserRepository repository, IValidator<UserCreateDto> validator)
    {
        _userRepository = repository;
        _userCreateDtoValidator = validator;
    }

    public async Task<long> CreateAsync(UserCreateDto userCreateDto)
    {
        var validationResult = await _userCreateDtoValidator.ValidateAsync(userCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingUser = await _userRepository.SelectByUserNameAsync(userCreateDto.UserName);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with username '{userCreateDto.UserName}' already exists.");
        }

        var salt = PasswordHasher.Hasher(userCreateDto.Password).Salt;
        var hashedPassword = PasswordHasher.Hasher(userCreateDto.Password).Hash;
        var user = MapService.MapUserCreateDtoToUser(userCreateDto, hashedPassword, salt);
        var userId = await _userRepository.InsertAsync(user);
        return userId;
    }

    public async Task DeleteAsync(long userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException($"User with ID {userId} not found.");
        }

        await _userRepository.DeleteAsync(userId);
    }

    public async Task DeleteAsync(UserGetDto user)
    {
        throw new NotImplementedException();

    }

    public ICollection<UserGetDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<UserGetDto>> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public async Task<UserGetDto> GetByIdAsync(long userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException($"User with ID {userId} not found.");
        }

        var userDto = MapService.MapUserToUserDto(user);
        return userDto;
    }

    public async Task<UserGetDto> GetByUserNameAsync(string userName)
    {
        var user = await _userRepository.SelectByUserNameAsync(userName);
        if (user == null)
        {
            throw new EntityNotFoundException($"User with username '{userName}' not found.");
        }
        var userDto = MapService.MapUserToUserDto(user);
        return userDto;
    }
}
