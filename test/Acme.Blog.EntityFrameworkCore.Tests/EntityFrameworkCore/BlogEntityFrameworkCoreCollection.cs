using Xunit;

namespace Acme.Blog.EntityFrameworkCore;

[CollectionDefinition(BlogTestConsts.CollectionDefinitionName)]
public class BlogEntityFrameworkCoreCollection : ICollectionFixture<BlogEntityFrameworkCoreFixture>
{
}