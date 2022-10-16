using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Infrastructure.Context
{
    public class DesignWritableDbContextFactory : IDesignTimeDbContextFactory<WritableDbContext>
    {
        public WritableDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<WritableDbContext>();
            var connectionString = configuration.GetConnectionString("WritableDbContext");
            builder.UseSqlServer(connectionString);
            return new WritableDbContext(builder.Options);
        }
    }
}
