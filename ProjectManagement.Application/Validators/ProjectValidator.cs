using FluentValidation;
using ProjectManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Validators
{
    public class ProjectCreateValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(x => x.EndDate ?? DateTime.MaxValue)
                .WithMessage("Start date must be before end date.");
        }
    }

    public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Project ID is required.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.EndDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("End date must be in the future.")
                .When(x => x.EndDate.HasValue);
        }
    }
}
