using Acme.Blog.Blog.Entities;
using System;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.IRepositories
{
	public interface ICategoryRepository : IRepository<Category, Guid>
	{
	}
}
