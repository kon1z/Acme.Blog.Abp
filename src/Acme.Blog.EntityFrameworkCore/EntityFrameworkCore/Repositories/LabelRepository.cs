using System;
using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories;

public class LabelRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
	: BlogRepositoryBase<Label, Guid>(dbContextProvider), ILabelRepository;