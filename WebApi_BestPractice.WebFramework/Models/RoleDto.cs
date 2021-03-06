﻿using FluentValidation;

namespace WebApi_BestPractice.WebApi.Models
{
    public class RoleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.Name).
                NotNull().
                MaximumLength(50);

            RuleFor(x => x.Description).
                NotNull().
                MaximumLength(100);
        }

    }

}
