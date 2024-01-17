using Acme.Blog.Samples;
using Xunit;

namespace Acme.Blog.EntityFrameworkCore.Domains;

[Collection(BlogTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BlogEntityFrameworkCoreTestModule>
{
}