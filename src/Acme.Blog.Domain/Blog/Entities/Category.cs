using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Blog.Blog.Entities
{
	public class Category : AggregateRoot<Guid>
	{
		public string Name { get; set; } = string.Empty;
	}
}
