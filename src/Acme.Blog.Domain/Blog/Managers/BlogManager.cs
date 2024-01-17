using Acme.Blog.Blog.IRepositories;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Blog.Managers;

public class BlogManager(IArticleRepository articleRepository) : DomainService
{
	private readonly IArticleRepository _articleRepository = articleRepository;
}