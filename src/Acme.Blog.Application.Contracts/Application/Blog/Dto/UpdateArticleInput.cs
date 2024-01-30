using Volo.Abp.Application.Dtos;

namespace Acme.Blog.Application.Blog.Dto;

public class UpdateArticleInput : EntityDto
{
    public string Content { get; set; } = null!;
}