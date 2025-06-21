using System.Collections.Generic;
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

public class SkillServiceBehaviorTests
{
    private readonly Mock<ISkillRepository> _skillRepositoryMock;
    private readonly Mock<IValidator<SkillCreateDto>> _createValidatorMock;
    private readonly Mock<IValidator<SkillUpdateDto>> _updateValidatorMock;
    private readonly SkillService _skillService;

    public SkillServiceBehaviorTests()
    {
        _skillRepositoryMock = new Mock<ISkillRepository>();
        _createValidatorMock = new Mock<IValidator<SkillCreateDto>>();
        _updateValidatorMock = new Mock<IValidator<SkillUpdateDto>>();

        _skillService = new SkillService(
            _skillRepositoryMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task PostAsync_Should_Validate_And_Insert_Skill()
    {
        // Arrange
        var dto = new SkillCreateDto { Name = "C#", Description = "Language" };
        _createValidatorMock.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        // Act
        await _skillService.PostAsync(dto);

        // Assert (behavior-based)
        _createValidatorMock.Verify(v => v.Validate(dto), Times.Once);
        _skillRepositoryMock.Verify(r => r.InsertAsync(It.Is<Skill>(s => s.Name == dto.Name)), Times.Once);
    }

    [Fact]
    public async Task PostAsync_Should_Not_Insert_When_Validation_Fails()
    {
        // Arrange
        var dto = new SkillCreateDto { Name = "", Description = "" };
        var failures = new List<ValidationFailure> { new ValidationFailure("Name", "Required") };

        _createValidatorMock.Setup(v => v.Validate(dto)).Returns(new ValidationResult(failures));

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _skillService.PostAsync(dto));

        _skillRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Skill>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_Should_Check_And_Delete_Skill()
    {
        // Arrange
        long skillId = 100;
        _skillRepositoryMock.Setup(r => r.SelectByIdAsync(skillId))
            .ReturnsAsync(new Skill { SkillId = skillId });

        // Act
        await _skillService.DeleteAsync(skillId);

        // Assert
        _skillRepositoryMock.Verify(r => r.SelectByIdAsync(skillId), Times.Once);
        _skillRepositoryMock.Verify(r => r.DeleteByIdAsync(skillId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Skill_Not_Found()
    {
        // Arrange
        long skillId = 404;
        _skillRepositoryMock.Setup(r => r.SelectByIdAsync(skillId)).ReturnsAsync((Skill)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _skillService.DeleteAsync(skillId));

        _skillRepositoryMock.Verify(r => r.DeleteByIdAsync(It.IsAny<long>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_Should_Validate_And_Update()
    {
        // Arrange
        var dto = new SkillUpdateDto { SkillId = 1, Name = "Updated" };
        _updateValidatorMock.Setup(v => v.Validate(dto)).Returns(new ValidationResult());

        // Act
        await _skillService.UpdateAsync(dto);

        // Assert
        _updateValidatorMock.Verify(v => v.Validate(dto), Times.Once);
        _skillRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Skill>(s => s.Name == dto.Name)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Should_Not_Update_When_Invalid()
    {
        // Arrange
        var dto = new SkillUpdateDto { SkillId = 1, Name = "" };
        var failures = new List<ValidationFailure> { new ValidationFailure("Name", "Required") };

        _updateValidatorMock.Setup(v => v.Validate(dto)).Returns(new ValidationResult(failures));

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _skillService.UpdateAsync(dto));

        _skillRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Skill>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Map_And_ReturnDto()
    {
        // Arrange
        var skill = new Skill { SkillId = 5, Name = "Go" };
        _skillRepositoryMock.Setup(r => r.SelectByIdAsync(skill.SkillId)).ReturnsAsync(skill);

        // Act
        var result = await _skillService.GetByIdAsync(skill.SkillId);

        // Assert
        _skillRepositoryMock.Verify(r => r.SelectByIdAsync(skill.SkillId), Times.Once);
        Assert.Equal("Go", result.Name);
    }
}
