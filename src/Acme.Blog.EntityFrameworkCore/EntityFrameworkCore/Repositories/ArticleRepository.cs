using System;
using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories;

public class ArticleRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
	: BlogRepositoryBase<Article, Guid>(dbContextProvider), IArticleRepository;