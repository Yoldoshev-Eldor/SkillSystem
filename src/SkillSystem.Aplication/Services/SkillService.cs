using FluentValidation;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Interfaces;
using SkillSystem.Core.Errors;

namespace SkillSystem.Aplication.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IValidator<SkillCreateDto> _skillCreateDtoValidator;
    private readonly IValidator<SkillUpdateDto> _skillUpdateDtoValidator;

    public SkillService(ISkillRepository skillRepository, IValidator<SkillCreateDto> skillCreateDtoValidator, IValidator<SkillUpdateDto> skillUpdateDtoValidator)
    {
        _skillRepository = skillRepository;
        _skillCreateDtoValidator = skillCreateDtoValidator;
        _skillUpdateDtoValidator = skillUpdateDtoValidator;
    }

    public async Task<long> PostAsync(SkillCreateDto skillCreateDto)
    {
        var validationResult = _skillCreateDtoValidator.Validate(skillCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var skill = MapService.MapSkillCreateDtoToSkill(skillCreateDto);
        await _skillRepository.InsertAsync(skill);
        return skill.SkillId;
    }

    public async Task DeleteAsync(long skillId)
    {
        _ = await _skillRepository.SelectByIdAsync(skillId) ?? throw new NotFoundException($"Skill with ID {skillId} not found.");
        await _skillRepository.DeleteByIdAsync(skillId);

    }

    public async Task<ICollection<SkillGetDto>> GetSkillsAsync(int skip, int take)
    {
        var skills = await _skillRepository.SelectSkillsAsync(skip, take);

        var skillDtos = skills.Select(s => MapService.MapSkillToSkillGetDto(s)).ToList();
        return skillDtos;

    }

    public ICollection<SkillGetDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<SkillGetDto> GetByIdAsync(long skillId)
    {
        var skill = await _skillRepository.SelectByIdAsync(skillId) ?? throw new NotFoundException($"Skill with ID {skillId} not found.");
        var skillDto = MapService.MapSkillToSkillGetDto(skill);
        return skillDto;
    }

    public async Task UpdateAsync(SkillUpdateDto skillUpdateDto)
    {
        var validationResult = _skillUpdateDtoValidator.Validate(skillUpdateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var skill = MapService.MapSkillUpdateDtoToSkill(skillUpdateDto);
        await _skillRepository.UpdateAsync(skill);
    }
}