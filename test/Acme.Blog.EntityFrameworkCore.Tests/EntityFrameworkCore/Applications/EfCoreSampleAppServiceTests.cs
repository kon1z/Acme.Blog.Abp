using Acme.Blog.Samples;
using Xunit;

namespace Acme.Blog.EntityFrameworkCore.Applications;

[Collection(BlogTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BlogEntityFrameworkCoreTestModule>
{
}