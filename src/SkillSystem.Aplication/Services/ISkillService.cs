using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public interface ISkillService
{
    Task<long> PostAsync(SkillCreateDto skillCreateDto);
    Task DeleteAsync(long skillId);
    Task<SkillGetDto> GetByIdAsync(long skillId);
    Task<ICollection<SkillGetDto>> GetSkillsAsync(int skip, int take);
    ICollection<SkillGetDto> GetAll();
    Task UpdateAsync(SkillUpdateDto skillUpdateDto);
}