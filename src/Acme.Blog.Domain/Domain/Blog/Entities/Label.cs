using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Domain.Blog.Entities;

public class Label : FullAuditedAggregateRoot<Guid>
{
    protected Label() { }

    public Label(string name)
    {
        Name = name;
    }
    public string Name { get; set; } = string.Empty;
}