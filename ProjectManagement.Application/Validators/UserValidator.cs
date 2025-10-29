using FluentValidation;
using ProjectManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Validators
{
    public class UserValidator : AbstractValidator<UserRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Full name is required");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Valid email is required");

            RuleFor(x => x.Password)
                .NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
