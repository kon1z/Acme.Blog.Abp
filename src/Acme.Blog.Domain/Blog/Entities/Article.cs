using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Blog.Entities;

public sealed class Article : FullAuditedAggregateRoot<Guid>
{
	/// <summary>
	/// for EfCore
	/// </summary>
	private Article()
	{
	}

	public Article(string title, string content)
	{
		Title = title;
		UpdateDescription(content);
		Content = new ArticleContent(content);
	}

	public string Title { get; set; } = null!;
	public string? Description { get; private set; }

	public ArticleContent? Content { get; }

	private void UpdateDescription(string content)
	{
		Description = content.Take(196) + "...";
	}

	public void UpdateContent(string content)
	{
		Check.NotNull(Content, "Article's content");

		UpdateDescription(content);
		Content!.Update(content);
	}
}