using System.Threading.Tasks;

namespace Acme.Data;

public interface IBlogDbSchemaMigrator
{
    Task MigrateAsync();
}