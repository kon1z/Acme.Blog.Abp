using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Entities;

public class Lable : FullAuditedAggregateRoot<Guid>
{
	public string Name { get; set; } = string.Empty;
}