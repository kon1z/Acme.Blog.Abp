using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using System;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories
{
    public class CategoryRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
		: BlogRepositoryBase<Category, Guid>(dbContextProvider), ICategoryRepository;
}
