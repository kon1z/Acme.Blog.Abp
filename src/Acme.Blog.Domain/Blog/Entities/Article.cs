using System;
using System.Linq;
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

	public Article(Guid id, string title, string content) : base(id)
	{
		Title = title;
		UpdateDescription(content);
		Content = new ArticleContent(content);
	}

	public string Title { get; set; } = null!;
	public string? Description { get; private set; }

	public ArticleContent? Content { get; private set; }

	private void UpdateDescription(string content)
	{
		Description = content.IsNullOrEmpty()
			? string.Empty
			: content.Length > 196
				? content.Take(196) + "..."
				: content;
	}

	internal void UpdateContent(string content)
	{
		UpdateDescription(content);
		if (Content != null)
		{
			Content.Update(content);
		}
		else
		{
			Content = new ArticleContent(content);
		}
	}
}