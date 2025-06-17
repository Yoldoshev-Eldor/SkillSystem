using FluentValidation;
using SkillSystem.Aplication.Dtos;

namespace SkillSystem.Application.FluentValidation;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ism bo‘sh bo‘lmasligi kerak.")
            .MaximumLength(50).WithMessage("Ism 50 belgidan oshmasligi kerak.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Familiya bo‘sh bo‘lmasligi kerak.")
            .MaximumLength(50).WithMessage("Familiya 50 belgidan oshmasligi kerak.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Foydalanuvchi nomi kerak.")
            .MinimumLength(3).WithMessage("UserName kamida 3 ta belgidan iborat bo‘lishi kerak.")
            .MaximumLength(30).WithMessage("UserName 30 ta belgidan oshmasligi kerak.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email kerak.")
            .EmailAddress().WithMessage("To‘g‘ri email manzil kiriting.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parol kerak.")
            .MinimumLength(6).WithMessage("Parol kamida 6 ta belgidan iborat bo‘lishi kerak.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon raqami kerak.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Telefon raqam formati noto‘g‘ri.");

    }
}
