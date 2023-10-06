using System;
using System.Threading.Tasks;
using Acme.Blog.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Blog.IAppServices;

public interface IBlogAppService : IApplicationService
{
    Task<PagedResultDto<ArticleDto>> GetListAsync(GetArticleDto input);
    Task<ArticleDetailDto> GetAsync(Guid id);
}