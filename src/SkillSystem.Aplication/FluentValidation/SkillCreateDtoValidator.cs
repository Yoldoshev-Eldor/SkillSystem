using FluentValidation;
using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Application.FluentValidation;

public class SkillCreateDtoValidator : AbstractValidator<SkillCreateDto>
{
    public SkillCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Skill nomi bo‘sh bo‘lmasligi kerak.")
            .MaximumLength(100).WithMessage("Skill nomi 100 belgidan oshmasligi kerak.");

        RuleFor(x => x.Level)
            .IsInEnum().WithMessage("Noto‘g‘ri skill darajasi tanlandi.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId 0 dan katta bo‘lishi kerak.");
    }
}
