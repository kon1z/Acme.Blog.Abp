using Acme.Blog.Domain.Blog.Entities;
using System;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Domain.Blog.IRepositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
