using ProjectName.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure;
public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
{
    public WriteDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.Development.json", optional: false)
          .Build();

        var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();
        optionsBuilder.UseMySql(
          config.GetConnectionString("DefaultConnection"),
          ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")),
          mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(WriteDbContext).Assembly.FullName)
        );

        return new WriteDbContext(optionsBuilder.Options);
    }
}
