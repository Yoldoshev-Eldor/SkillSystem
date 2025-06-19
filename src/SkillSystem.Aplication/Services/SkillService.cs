using FluentValidation;
using SkillSystem.Aplication.Dtos;
using SkillSystem.Aplication.Interfaces;

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
        var skill=await _skillRepository.SelectByIdAsync(skillId);
        await _skillRepository.DeleteByIdAsync(skillId);

    }

    public ICollection<SkillGetDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<SkillGetDto> GetByIdAsync(long skillId)
    {
        throw new NotImplementedException();
    }



    public Task UpdateAsync(SkillUpdateDto skillUpdateDto)
    {
        throw new NotImplementedException();
    }
}
