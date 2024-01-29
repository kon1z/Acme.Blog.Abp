using Volo.Abp.Application.Dtos;

namespace Acme.Blog.Application.Blog.Dto;

public class CreateArticleInput : EntityDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}