using System.Threading.Tasks;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IRepositories;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Blog.Managers;

public class BlogManager(IArticleRepository articleRepository) : DomainService
{
	private readonly IArticleRepository _articleRepository = articleRepository;

	public async Task<Article> CreateArticleAsync(string title, string content)
	{
		return await _articleRepository.InsertAsync(new Article(title, content));
	}
}