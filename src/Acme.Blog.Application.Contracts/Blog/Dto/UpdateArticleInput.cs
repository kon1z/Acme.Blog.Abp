using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Blog.Blog.AppServices;

public class UpdateArticleInput : EntityDto<Guid>
{
	public string Content { get; set; } = null!;
}