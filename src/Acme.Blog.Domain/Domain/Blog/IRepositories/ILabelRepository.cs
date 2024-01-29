using System;
using Acme.Blog.Domain.Blog.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Domain.Blog.IRepositories;

public interface ILabelRepository : IRepository<Label, Guid>
{
}