using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IRepositories;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Blog.Managers;

[UsedImplicitly]
public class BlogManager(IArticleRepository articleRepository) : DomainService
{
	public IReadOnlyRepository<Article, Guid> ArticleReadOnlyRepository =>
		LazyServiceProvider.LazyGetRequiredService<IReadOnlyRepository<Article, Guid>>();

	public async Task<Article> CreateArticleAsync(string title, string content)
	{
		return await articleRepository.InsertAsync(new Article(title, content));
	}
}