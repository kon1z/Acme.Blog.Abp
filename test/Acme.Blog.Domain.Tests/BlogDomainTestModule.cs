using Volo.Abp.Modularity;

namespace Acme.Blog;

[DependsOn(
	typeof(BlogDomainModule),
	typeof(BlogTestBaseModule)
)]
public class BlogDomainTestModule : AbpModule
{
}