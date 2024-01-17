using System;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IRepositories;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Blog.EntityFrameworkCore.Repositories;

public class LabelRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
	: BlogRepositoryBase<Label, Guid>(dbContextProvider), ILabelRepository;