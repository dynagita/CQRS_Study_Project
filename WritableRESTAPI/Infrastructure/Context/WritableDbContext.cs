using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Infrastructure.ContextMap;

namespace WritableRESTAPI.Infrastructure.Context
{
    public class WritableDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public WritableDbContext(DbContextOptions<WritableDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
        }
    }
}
