using Volo.Abp.Modularity;

namespace Acme.Blog;

/* Inherit from this class for your domain layer tests. */
public abstract class BlogDomainTestBase<TStartupModule> : BlogTestBase<TStartupModule>
	where TStartupModule : IAbpModule
{
}