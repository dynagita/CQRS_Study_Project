using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;

namespace WritableRESTAPI.Infrastructure.ContextMap
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subject)
                .IsRequired(true);

            builder.Property(x => x.Abstract)
                .IsRequired(true);

            builder.Property(x => x.Content)
                .IsRequired(true);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Article)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Author)
                .WithMany(z => z.Articles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Active)
                .IsRequired(true);
        }
    }
}
