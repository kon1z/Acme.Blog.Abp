using Volo.Abp.Application.Dtos;

namespace Acme.Blog.Blog.Dto;

public class CreateArticleInput : EntityDto
{
	public string Title { get; set; } = null!;
	public string Content { get; set; } = null!;
}