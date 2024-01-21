using Acme.Blog.Blog.Dto;

namespace Acme.Blog.Blog.Dto;

public class ArticleDetailDto : ArticleDto
{
	public string Content { get; set; } = string.Empty;
}