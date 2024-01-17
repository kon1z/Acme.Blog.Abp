using System;
using Acme.Blog.Blog.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.IRepositories;

public interface IArticleRepository : IRepository<Article, Guid>
{
}