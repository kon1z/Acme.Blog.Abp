using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Blog.AppServices;

public class ArticleDto : EntityDto<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateTime { get; set; }
}