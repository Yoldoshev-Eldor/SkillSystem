using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Aplication.Services;
using SkillSystem.Domain.Entities;
using SkillSystem.Domain.Errors;
using Xunit;

public class UserServiceTesting
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IValidator<UserCreateDto>> _validatorMock = new();
    private readonly UserService _userService;

    public UserServiceTesting()
    {
        _userService = new UserService(_userRepositoryMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Validation_Fails()
    {
        // Arrange
        var userDto = new UserCreateDto { UserName = "testuser", Password = "123" };
        var validationResult = new ValidationResult(new[] { new ValidationFailure("UserName", "Required") });

        _validatorMock.Setup(v => v.ValidateAsync(userDto, default)).ReturnsAsync(validationResult);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _userService.CreateAsync(userDto));
        _validatorMock.Verify(v => v.ValidateAsync(userDto, default), Times.Once);
        _userRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_UserName_Already_Exists()
    {
        // Arrange
        var userDto = new UserCreateDto { UserName = "existinguser", Password = "123456" };
        var validationResult = new ValidationResult(); // valid

        _validatorMock.Setup(v => v.ValidateAsync(userDto, default)).ReturnsAsync(validationResult);
        _userRepositoryMock.Setup(r => r.SelectByUserNameAsync(userDto.UserName)).ReturnsAsync(new User());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.CreateAsync(userDto));
        _userRepositoryMock.Verify(r => r.SelectByUserNameAsync(userDto.UserName), Times.Once);
        _userRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<User>()), Times.Never);
    }

   
    [Fact]
    public async Task DeleteAsync_Should_Throw_If_User_Not_Found()
    {
        // Arrange
        long userId = 99;
        _userRepositoryMock.Setup(r => r.SelectByIdAsync(userId)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.DeleteAsync(userId));
        _userRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<long>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_Delete_If_User_Exists()
    {
        // Arrange
        var user = new User { UserId = 7, UserName = "deleteMe" };
        _userRepositoryMock.Setup(r => r.SelectByIdAsync(user.UserId)).ReturnsAsync(user);

        // Act
        await _userService.DeleteAsync(user.UserId);

        // Assert
        _userRepositoryMock.Verify(r => r.DeleteAsync(user.UserId), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Throw_If_Not_Found()
    {
        // Arrange
        long userId = 77;
        _userRepositoryMock.Setup(r => r.SelectByIdAsync(userId)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.GetByIdAsync(userId));
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_UserDto_If_Found()
    {
        // Arrange
        var user = new User { UserId = 1, UserName = "foundUser" };
        _userRepositoryMock.Setup(r => r.SelectByIdAsync(user.UserId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(user.UserId);

        // Assert
        Assert.Equal(user.UserName, result.UserName);
        _userRepositoryMock.Verify(r => r.SelectByIdAsync(user.UserId), Times.Once);
    }

    [Fact]
    public async Task GetByUserNameAsync_Should_Throw_If_Not_Found()
    {
        // Arrange
        string userName = "unknown";
        _userRepositoryMock.Setup(r => r.SelectByUserNameAsync(userName)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.GetByUserNameAsync(userName));
    }

    [Fact]
    public async Task GetByUserNameAsync_Should_Return_UserDto_If_Found()
    {
        // Arrange
        string userName = "known";
        var user = new User { UserId = 3, UserName = userName };
        _userRepositoryMock.Setup(r => r.SelectByUserNameAsync(userName)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetByUserNameAsync(userName);

        // Assert
        Assert.Equal(userName, result.UserName);
        _userRepositoryMock.Verify(r => r.SelectByUserNameAsync(userName), Times.Once);
    }
}
