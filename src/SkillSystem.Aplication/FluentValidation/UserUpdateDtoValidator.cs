using FluentValidation;
using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Application.FluentValidation;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId 0 dan katta bo‘lishi kerak.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ism bo‘sh bo‘lmasligi kerak.")
            .MaximumLength(50).WithMessage("Ism 50 belgidan oshmasligi kerak.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Familiya bo‘sh bo‘lmasligi kerak.")
            .MaximumLength(50).WithMessage("Familiya 50 belgidan oshmasligi kerak.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName kerak.")
            .MinimumLength(3).WithMessage("UserName kamida 3 ta belgidan iborat bo‘lishi kerak.")
            .MaximumLength(30).WithMessage("UserName 30 ta belgidan oshmasligi kerak.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email kerak.")
            .EmailAddress().WithMessage("To‘g‘ri email manzil kiriting.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon raqami kerak.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Telefon raqam formati noto‘g‘ri.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Noto‘g‘ri rol tanlandi.");
    }
}
