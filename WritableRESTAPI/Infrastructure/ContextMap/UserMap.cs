using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;

namespace WritableRESTAPI.Infrastructure.ContextMap
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Active)
                .IsRequired(true);

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Articles)
                .WithOne(x => x.Author)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
