using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Blog.AppServices;

public class ArticleDto : EntityDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
}