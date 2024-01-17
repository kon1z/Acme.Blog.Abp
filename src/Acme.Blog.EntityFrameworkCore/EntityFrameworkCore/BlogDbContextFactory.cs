using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Acme.Blog.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
	public BlogDbContext CreateDbContext(string[] args)
	{
		// https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		BlogEfCoreEntityExtensionMappings.Configure();

		var configuration = BuildConfiguration();

		var builder = new DbContextOptionsBuilder<BlogDbContext>()
			.UseNpgsql(configuration.GetConnectionString("Default"));

		return new BlogDbContext(builder.Options);
	}

	private static IConfigurationRoot BuildConfiguration()
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Acme.Blog.DbMigrator/"))
			.AddJsonFile("appsettings.json", false);

		return builder.Build();
	}
}