using System;
using Acme.Blog.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.IRepositories;

public interface IArticleRepository : IRepository<Article, Guid>
{
}