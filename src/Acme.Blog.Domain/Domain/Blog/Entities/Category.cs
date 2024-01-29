using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Blog.Domain.Blog.Entities
{
    public class Category : AggregateRoot<Guid>
    {
        protected Category() { }

        public Category(string name)
        {
            Name = name;
        }
        public string Name { get; set; } = string.Empty;
    }
}
