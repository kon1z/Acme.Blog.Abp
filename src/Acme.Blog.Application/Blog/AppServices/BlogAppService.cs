using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.Blog.Blog.Dtos;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IAppServices;
using Acme.Blog.Blog.IRepositories;
using Acme.Blog.Blog.Managers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.AppServices;

public class BlogAppService(
	IArticleRepository articleRepository,
	BlogManager blogManager)
	: BlogAppServiceBase, IBlogAppService
{
	private readonly BlogManager _blogManager = blogManager;

	public async Task<PagedResultDto<ArticleDto>> GetListAsync(GetArticleDto input)
	{
		var items = await articleRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, "Id");
		var totalCount = await articleRepository.LongCountAsync();

		return new PagedResultDto<ArticleDto>(totalCount, ObjectMapper.Map<List<Article>, List<ArticleDto>>(items));
	}

	public async Task<ArticleDetailDto> GetAsync(Guid id)
	{
		var article = await articleRepository.GetAsync(id);

		return ObjectMapper.Map<Article, ArticleDetailDto>(article);
	}
}