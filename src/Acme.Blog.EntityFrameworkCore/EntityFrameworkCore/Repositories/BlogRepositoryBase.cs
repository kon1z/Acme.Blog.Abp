using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories;

public abstract class BlogRepositoryBase<TEntity> : EfCoreRepository<BlogDbContext, TEntity>
	where TEntity : class, IEntity
{
	protected BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
	{
	}
}

public abstract class BlogRepositoryBase<TEntity, TKey> : EfCoreRepository<BlogDbContext, TEntity, TKey>
	where TEntity : class, IEntity<TKey>
{
	protected BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
	{
	}
}