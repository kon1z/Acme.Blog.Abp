using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.Blog.Dtos;
using Acme.Blog.Entities;
using Acme.Blog.IAppServices;
using Acme.Blog.IRepositories;
using Acme.Blog.Managers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.AppServices;

public class BlogAppService : AcmeBlogAppService, IBlogAppService
{
	private readonly IArticleRepository _articleRepository;
	private readonly BlogManager _blogManager;

	public BlogAppService(
		IArticleRepository articleRepository,
		BlogManager blogManager)
	{
		_articleRepository = articleRepository;
		_blogManager = blogManager;
	}

	public async Task<PagedResultDto<ArticleDto>> GetListAsync(GetArticleDto input)
	{
		var items = await _articleRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, "Id");
		var totalCount = await _articleRepository.LongCountAsync();

		return new PagedResultDto<ArticleDto>(totalCount, ObjectMapper.Map<List<Article>, List<ArticleDto>>(items));
	}

	public async Task<ArticleDetailDto> GetAsync(Guid id)
	{
		var article = await _articleRepository.GetAsync(id);

		return ObjectMapper.Map<Article, ArticleDetailDto>(article);
	}
}