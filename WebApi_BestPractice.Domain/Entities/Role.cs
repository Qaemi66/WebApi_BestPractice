using System.ComponentModel.DataAnnotations;

namespace WebApi_BestPractice.Domain.Etities
{
    public class Role : BaseClasses.BaseEntity {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
