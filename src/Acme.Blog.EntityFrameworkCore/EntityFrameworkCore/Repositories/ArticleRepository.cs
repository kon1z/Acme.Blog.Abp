using System;
using Acme.Blog.Entities;
using Acme.Blog.IRepositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.EntityFrameworkCore.Repositories;

public class ArticleRepository : EfCoreRepository<BlogDbContext, Article, Guid>, IArticleRepository
{
	public ArticleRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
	{
	}
}