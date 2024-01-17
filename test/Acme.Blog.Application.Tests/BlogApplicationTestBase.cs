using Volo.Abp.Modularity;

namespace Acme.Blog;

public abstract class BlogApplicationTestBase<TStartupModule> : BlogTestBase<TStartupModule>
	where TStartupModule : IAbpModule
{
}