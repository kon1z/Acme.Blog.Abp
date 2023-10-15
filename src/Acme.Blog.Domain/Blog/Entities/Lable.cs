using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Entities
{
    public class Lable : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
