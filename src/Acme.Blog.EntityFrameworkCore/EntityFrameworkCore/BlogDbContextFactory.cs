using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Acme.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        BlogEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BlogDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new BlogDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Acme.Blog.DbMigrator/"))
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
<<<<<<< Updated upstream
    }
}
=======
    } 
}
>>>>>>> Stashed changes
