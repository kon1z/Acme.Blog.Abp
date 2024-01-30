using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Blog.Blog.Entities
{
    public class Visit : FullAuditedAggregateRoot<Guid>
    {
        protected Visit()
        {
        }
        public Visit(string ip, Guid articleId)
        {
            IP = ip;
            ArticleId = articleId;
        }

        public string IP { get; set; }


        public Guid ArticleId { get; set; }
    }
}
