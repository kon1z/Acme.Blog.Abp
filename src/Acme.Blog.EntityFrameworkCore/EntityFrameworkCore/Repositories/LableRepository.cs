using System;
using Acme.Blog.Entities;
using Acme.Blog.IRepositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.EntityFrameworkCore.Repositories;

public class LableRepository : EfCoreRepository<BlogDbContext, Lable, Guid>, ILableRepository
{
	public LableRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
	{
	}
}