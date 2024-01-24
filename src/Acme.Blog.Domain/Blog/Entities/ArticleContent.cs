using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Blog.Blog.Entities
{
	public class ArticleContent : Entity<Guid>
	{
		/// <summary>
		/// for EfCore
		/// </summary>
		private ArticleContent()
		{
		}

		public ArticleContent(string content)
		{
			Content = content;
		}

		public Guid ArticleId { get; private set; }
		public string Content { get; private set; } = null!;

		internal void Update(string content)
		{
			Content = content;
		}
	}
}
