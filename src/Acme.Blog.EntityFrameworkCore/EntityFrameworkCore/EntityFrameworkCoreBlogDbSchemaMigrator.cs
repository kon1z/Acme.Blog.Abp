using System;
using System.Threading.Tasks;
using Acme.Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Acme.Blog.EntityFrameworkCore;

public class EntityFrameworkCoreBlogDbSchemaMigrator(IServiceProvider serviceProvider)
	: IBlogDbSchemaMigrator, ITransientDependency
{
	public async Task MigrateAsync()
	{
		/* We intentionally resolve the BlogDbContext
		 * from IServiceProvider (instead of directly injecting it)
		 * to properly get the connection string of the current tenant in the
		 * current scope.
		 */

		await serviceProvider
			.GetRequiredService<BlogDbContext>()
			.Database
			.MigrateAsync();
	}
}