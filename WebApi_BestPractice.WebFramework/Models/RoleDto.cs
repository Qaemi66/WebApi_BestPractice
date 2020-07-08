using FluentValidation;
using System.Text.RegularExpressions;
using WebApi_BestPractice.Domain.Enums;

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
                Length(50);

            RuleFor(x => x.Description).
                NotNull().
                Length(100);
        }

    }

}
