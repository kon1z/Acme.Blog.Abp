using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Blog.Entities;

public class Label : FullAuditedAggregateRoot<Guid>
{
	public string Name { get; set; } = string.Empty;
}