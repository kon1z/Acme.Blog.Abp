using Acme.Blog.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.Blog;

[DependsOn(
    typeof(BlogEntityFrameworkCoreTestModule)
    )]
public class BlogDomainTestModule : AbpModule
{

}
