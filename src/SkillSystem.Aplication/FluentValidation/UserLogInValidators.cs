using FluentValidation;
using SkillSystem.Aplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Aplication.FluentValidation;

public class UserLogInValidators : AbstractValidator<UserLogInDto>
{
    public UserLogInValidators()
    {
        RuleFor(x => x.UserName)
          .NotEmpty()
          .WithMessage("UserName is required")
          .Length(3, 20)
          .WithMessage("UserName must be between 3 and 20 characters long");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Length(8, 20)
            .WithMessage("Password must be between 8 and 20 characters long");
    }
}
