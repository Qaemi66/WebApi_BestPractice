using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace WebApi_BestPractice.Domain.Entities
{
    public class Category : BaseClasses.BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.HasOne(p => p.ParentCategory).WithMany(p => p.ChildCategories).HasForeignKey(p=>p.ParentCategoryId);
        }
    }
}
