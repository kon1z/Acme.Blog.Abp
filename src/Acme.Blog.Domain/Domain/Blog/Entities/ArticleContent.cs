using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Blog.Domain.Blog.Entities
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
            Update(content);
        }

        public Guid ArticleId { get; private set; }
        public string Content { get; private set; } = null!;

        internal void Update(string content)
        {
            Content = content;
        }
    }
}
