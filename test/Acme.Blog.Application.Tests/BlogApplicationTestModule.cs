using Volo.Abp.Modularity;

namespace Acme;

[DependsOn(
    typeof(BlogApplicationModule),
    typeof(BlogDomainTestModule)
)]
public class BlogApplicationTestModule : AbpModule
{
}