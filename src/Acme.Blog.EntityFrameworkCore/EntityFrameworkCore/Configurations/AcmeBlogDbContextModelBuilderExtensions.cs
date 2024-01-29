using Acme.Blog.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.Blog.EntityFrameworkCore.Configurations
{
    public static class AcmeBlogDbContextModelBuilderExtensions
	{
		public static void ConfigureBlog(this ModelBuilder builder)
		{
			builder.Entity<Article>(b =>
			{
				b.ToTable(BlogConstants.DbTablePrefix + "Articles" + BlogConstants.DbSchema);

				b.ConfigureByConvention();

				b.Property(x => x.Title).HasMaxLength(BlogConstants.ArticleTitleMaxLength).IsRequired();
				b.Property(x => x.Description).HasMaxLength(200);

				b.HasOne(a => a.Content).WithOne().HasForeignKey<ArticleContent>(ac => ac.ArticleId);

				b.HasIndex(x => x.Title);

				b.ApplyObjectExtensionMappings();
			});

			builder.Entity<ArticleContent>(b =>
			{
				b.ToTable(BlogConstants.DbTablePrefix + "ArticleContents" + BlogConstants.DbSchema);

				b.ConfigureByConvention();

				b.Property(x => x.Content).HasMaxLength(BlogConstants.ArticleContentMaxLength).IsRequired();

				b.HasOne<Article>().WithOne().HasForeignKey<ArticleContent>(a => a.ArticleId);

				b.ApplyObjectExtensionMappings();
			});
		}
	}
}
