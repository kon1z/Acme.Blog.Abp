using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Entities;

public class Article : FullAuditedAggregateRoot<Guid>
{
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
}