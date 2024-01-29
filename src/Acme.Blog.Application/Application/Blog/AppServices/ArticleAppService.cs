using Acme.Blog.Application.Blog.Dto;
using Acme.Blog.Application.Blog.IAppServices;
using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Application.Blog.AppServices;

public class ArticleAppService(ArticleManager articleManager) : BlogAppServiceBase, IBlogAppService
{

    public async Task<PagedResultDto<ArticleDto>> GetListAsync(GetArticleDto input)
    {
        var items = await articleManager.ArticleRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, "Id");
        var totalCount = await articleManager.ArticleRepository.LongCountAsync();

        return new PagedResultDto<ArticleDto>(totalCount, ObjectMapper.Map<List<Article>, List<ArticleDto>>(items));
    }

    public async Task<ArticleDetailDto> GetAsync(Guid id)
    {
        var article = await articleManager.ArticleRepository.GetAsync(id);

        return ObjectMapper.Map<Article, ArticleDetailDto>(article);
    }

    public async Task<ArticleDetailDto> CreateAsync(CreateArticleInput input)
    {
        var article = await articleManager.CreateArticleAsync(new Article(input.Title, input.Content));

        return ObjectMapper.Map<Article, ArticleDetailDto>(article);
    }

    public async Task<ArticleDetailDto> UpdateAsync(Guid id, UpdateArticleInput input)
    {
        var article = await articleManager.ArticleRepository.GetAsync(id);

        articleManager.UpdateArticleContentAsync(article, input.Content);

        return ObjectMapper.Map<Article, ArticleDetailDto>(article);
    }
}