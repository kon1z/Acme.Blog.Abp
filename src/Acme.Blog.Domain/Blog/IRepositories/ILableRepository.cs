using Acme.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.IRepositories
{
    public interface ILableRepository : IRepository<Lable,Guid>
    {
    }
}
