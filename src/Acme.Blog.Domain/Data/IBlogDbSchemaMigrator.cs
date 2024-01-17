using System.Threading.Tasks;

namespace Acme.Blog.Data;

public interface IBlogDbSchemaMigrator
{
	Task MigrateAsync();
}