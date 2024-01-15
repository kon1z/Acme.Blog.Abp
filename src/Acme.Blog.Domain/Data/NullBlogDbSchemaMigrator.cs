using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.Data;

/* This is used if database provider does't define
 * IBlogDbSchemaMigrator implementation.
 */
public class NullBlogDbSchemaMigrator : IBlogDbSchemaMigrator, ITransientDependency
{
	public Task MigrateAsync()
	{
		return Task.CompletedTask;
	}
}