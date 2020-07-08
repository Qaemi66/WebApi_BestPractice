using FluentValidation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApi_BestPractice.Domain.Enums;

namespace WebApi_BestPractice.WebApi.Models
{
    public class UserDto  
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
        
        public string FullName { get; set; }
        
        public int Age { get; set; }
        
        public GenderType Gender { get; set; }

       // public ICollection<RoleDto> Roles { get; set; }

        /*جهت پياده سازي وليديشن ديفالت دات نت و افزودن يك وليديشن جديد
         public class UserDto :IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test",StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام كاربري نمي تواند test باشد", new[]{ nameof(UserName) });
          
            if (Password.Equals("1234",StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("كلمه عبور نمي تواند 1234 باشد", new[]{ nameof(Password) });

        }
        */
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName).
                NotNull().
                MaximumLength(100);

            RuleFor(x => x.FullName).
                NotNull().
                NotEqual("test").WithMessage("نام كاربري نمي تواند test باشد").
                MaximumLength(100);

            RuleFor(x => x.Password).
                NotNull().
                NotEqual("test").WithMessage("كلمه عبور نمي تواند 1234 باشد").
                Must(hasValidPassword);

            RuleFor(x => x.ConfirmPassword).
                Equal(x => x.Password).
                WithMessage("كلمه عبور و تكرار آن يكسان نيست");

            RuleFor(x => x.Age).InclusiveBetween(18, 60);
            
           // RuleForEach(x => x.Roles).SetValidator(new RoleDtoValidator());

        }

        private bool hasValidPassword(string password)
        {
            return true;
            /*
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");
            return lowercase.IsMatch(password) &&
                uppercase.IsMatch(password) &&
                digit.IsMatch(password) &&
                symbol.IsMatch(password);
            */
        }
    }

}
