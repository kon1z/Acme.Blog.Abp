using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.IRepositories
{
    public interface ICategoryRepository : IRepository<Category,Guid>
    {
    }
}
