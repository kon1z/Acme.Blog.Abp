﻿using Acme.Blog.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Acme.Blog.EntityFrameworkCore
{
    public partial class BlogDbContext
	{
		/* Add DbSet properties for your Aggregate Roots / Entities here. */

		public DbSet<Article> Articles { get; set; }
		public DbSet<ArticleContent> ArticleContents { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Label> Labels { get; set; }

		#region Entities from the modules

		/* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
		 * and replaced them for this DbContext. This allows you to perform JOIN
		 * queries for the entities of these modules over the repositories easily. You
		 * typically don't need that for other modules. But, if you need, you can
		 * implement the DbContext interface of the needed module and use ReplaceDbContext
		 * attribute just like IIdentityDbContext and ITenantManagementDbContext.
		 *
		 * More info: Replacing a DbContext of a module ensures that the related module
		 * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
		 */

		//Identity
		public DbSet<IdentityUser> Users { get; set; }
		public DbSet<IdentityRole> Roles { get; set; }
		public DbSet<IdentityClaimType> ClaimTypes { get; set; }
		public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
		public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
		public DbSet<IdentityLinkUser> LinkUsers { get; set; }
		public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

		// Tenant Management
		public DbSet<Tenant> Tenants { get; set; }
		public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

		#endregion
	}
}
