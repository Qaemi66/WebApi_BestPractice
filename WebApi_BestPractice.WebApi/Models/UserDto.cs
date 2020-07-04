using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Enums;

namespace WebApi_BestPractice.WebApi.Models
{
    public class UserDto :IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test",StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام كاربري نمي تواند test باشد", new[]{ nameof(UserName) });
            
            if (Password.Equals("1234",StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("كلمه عبور نمي تواند 1234 باشد", new[]{ nameof(Password) });

        }
    }

}
