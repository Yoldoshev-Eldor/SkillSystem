using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Aplication.Services;

public class SkillService : ISkillService
{
    public Task DeleteAsync(long skillId)
    {
        throw new NotImplementedException();
    }

    public ICollection<SkillGetDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<SkillGetDto> GetByIdAsync(long skillId)
    {
        throw new NotImplementedException();
    }

    public Task<long> PostAsync(SkillCreateDto skillCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SkillUpdateDto skillUpdateDto)
    {
        throw new NotImplementedException();
    }
}
