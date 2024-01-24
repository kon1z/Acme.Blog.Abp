using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IRepositories;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Blog.Managers;

[UsedImplicitly]
public class BlogManager : DomainService
{
	public IArticleRepository ArticleRepository => 
		LazyServiceProvider.LazyGetRequiredService<IArticleRepository>();

	public async Task<Article> CreateArticleAsync(Article article)
	{
		return await ArticleRepository.InsertAsync(article);
	}

	public void UpdateArticleContentAsync(Article article, string content)
	{
		article.UpdateContent(content);
	}
}