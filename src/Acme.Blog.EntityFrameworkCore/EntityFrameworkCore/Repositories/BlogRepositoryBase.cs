using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories;

public abstract class BlogRepositoryBase<TEntity>(IDbContextProvider<BlogDbContext> dbContextProvider)
	: EfCoreRepository<BlogDbContext, TEntity>(dbContextProvider)
	where TEntity : class, IEntity;

public abstract class BlogRepositoryBase<TEntity, TKey>(IDbContextProvider<BlogDbContext> dbContextProvider)
	: EfCoreRepository<BlogDbContext, TEntity, TKey>(dbContextProvider)
	where TEntity : class, IEntity<TKey>;