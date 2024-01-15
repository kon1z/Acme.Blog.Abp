using Acme.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme;

[DependsOn(
	typeof(BlogEntityFrameworkCoreTestModule)
)]
public class BlogDomainTestModule : AbpModule
{
}