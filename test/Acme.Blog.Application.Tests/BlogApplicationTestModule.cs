using Volo.Abp.Modularity;

namespace Acme.Blog;

[DependsOn(
    typeof(BlogApplicationModule),
    typeof(BlogDomainTestModule)
    )]
public class BlogApplicationTestModule : AbpModule
{

}
