using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WebApi_BestPractice.Domain.Etities
{
    public class Post : BaseClasses.BaseEntity<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }


        public Category Category { get; set; }

        public User Author { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.Posts).HasForeignKey(p=>p.CategoryId);
            builder.HasOne(p => p.Author).WithMany(p=>p.Posts).HasForeignKey(p=>p.AuthorId);
        }
    }
}
