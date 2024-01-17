using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IRepositories;
using System;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories
{
	public class CategoryRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
		: BlogRepositoryBase<Category, Guid>(dbContextProvider), ICategoryRepository;
}
