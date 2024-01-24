using Acme.Blog.Blog.Dto;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IAppServices;
using Acme.Blog.Blog.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.AppServices;

public class BlogAppService(BlogManager blogManager) : BlogAppServiceBase, IBlogAppService
{
	public async Task<PagedResultDto<ArticleDto>> GetListAsync(GetArticleDto input)
	{
		var items = await blogManager.ArticleReadOnlyRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, "Id");
		var totalCount = await blogManager.ArticleReadOnlyRepository.LongCountAsync();

		return new PagedResultDto<ArticleDto>(totalCount, ObjectMapper.Map<List<Article>, List<ArticleDto>>(items));
	}

	public async Task<ArticleDetailDto> GetAsync(Guid id)
	{
		var article = await blogManager.ArticleReadOnlyRepository.GetAsync(id);

		return ObjectMapper.Map<Article, ArticleDetailDto>(article);
	}

	public async Task<ArticleDetailDto> CreateAsync(CreateArticleInput input)
	{
		var article = await blogManager.CreateArticleAsync(input.Title, input.Content);
		return ObjectMapper.Map<Article, ArticleDetailDto>(article);
	}
}