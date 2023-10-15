using Acme.Blog.Entities;
using Acme.Blog.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.EntityFrameworkCore.Repositories
{
    public class LableRepository : EfCoreRepository<BlogDbContext,Lable,Guid>,ILableRepository
    {
        public LableRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
 
        }
    }
}
