using Acme.Blog.IRepositories;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Managers;

public class BlogManager : DomainService
{
	private readonly IArticleRepository _articleRepository;

	public BlogManager(IArticleRepository articleRepository)
	{
		_articleRepository = articleRepository;
	}
}