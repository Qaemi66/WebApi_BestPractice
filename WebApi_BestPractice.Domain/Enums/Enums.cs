using System.ComponentModel.DataAnnotations;

namespace WebApi_BestPractice.Domain.Enums
{
    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}
