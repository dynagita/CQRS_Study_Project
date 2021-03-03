using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;

namespace WritableRESTAPI.Infrastructure.ContextMap
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(z => z.Likes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Article)
                .WithMany(x => x.Likes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Active)
                .IsRequired(true);

            
        }
    }
}
