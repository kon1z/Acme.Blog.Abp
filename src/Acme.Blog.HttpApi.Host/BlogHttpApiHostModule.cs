using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acme.EntityFrameworkCore;
using Acme.MultiTenancy;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace Acme;

[DependsOn(
	typeof(BlogHttpApiModule),
	typeof(AbpAutofacModule),
	typeof(AbpCachingStackExchangeRedisModule),
	typeof(AbpDistributedLockingModule),
	typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
	typeof(BlogApplicationModule),
	typeof(BlogEntityFrameworkCoreModule),
	typeof(AbpAspNetCoreSerilogModule),
	typeof(AbpSwashbuckleModule)
)]
public class BlogHttpApiHostModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		ConfigureConventionalControllers();
		ConfigureAuthentication(context, configuration);
		ConfigureCache(configuration);
		ConfigureVirtualFileSystem(context);
		ConfigureDataProtection(context, configuration, hostingEnvironment);
		ConfigureDistributedLocking(context, configuration);
		ConfigureCors(context, configuration);
		ConfigureSwaggerServices(context, configuration);
	}

	private void ConfigureCache(IConfiguration configuration)
	{
		Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Blog:"; });
	}

	private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		if (hostingEnvironment.IsDevelopment())
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainSharedModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Domain.Shared"));
				options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Domain"));
				options.FileSets.ReplaceEmbeddedByPhysical<BlogApplicationContractsModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Application.Contracts"));
				options.FileSets.ReplaceEmbeddedByPhysical<BlogApplicationModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Application"));
			});
	}

	private void ConfigureConventionalControllers()
	{
		Configure<AbpAspNetCoreMvcOptions>(options =>
		{
			options.ConventionalControllers.Create(typeof(BlogApplicationModule).Assembly);
		});
	}

	private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = configuration["AuthServer:Authority"];
				options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
				options.Audience = "Blog";
			});
	}

	private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddAbpSwaggerGenWithOAuth(
			configuration["AuthServer:Authority"],
			new Dictionary<string, string>
			{
				{ "Blog", "Blog API" }
			},
			options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
				options.DocInclusionPredicate((docName, description) => true);
				options.CustomSchemaIds(type => type.FullName);
			});
	}

	private void ConfigureDataProtection(
		ServiceConfigurationContext context,
		IConfiguration configuration,
		IWebHostEnvironment hostingEnvironment)
	{
		var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Blog");
		if (!hostingEnvironment.IsDevelopment())
		{
			var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Blog-Protection-Keys");
		}
	}

	private void ConfigureDistributedLocking(
		ServiceConfigurationContext context,
		IConfiguration configuration)
	{
		context.Services.AddSingleton<IDistributedLockProvider>(sp =>
		{
			var connection = ConnectionMultiplexer
				.Connect(configuration["Redis:Configuration"]!);
			return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
		});
	}

	private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.WithOrigins(configuration["App:CorsOrigins"]?
						.Split(",", StringSplitOptions.RemoveEmptyEntries)
						.Select(o => o.RemovePostFix("/"))
						.ToArray() ?? Array.Empty<string>())
					.WithAbpExposedHeaders()
					.SetIsOriginAllowedToAllowWildcardSubdomains()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();

		if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

		app.UseAbpRequestLocalization();
		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseCors();
		app.UseAuthentication();

		if (MultiTenancyConsts.IsEnabled) app.UseMultiTenancy();

		app.UseAuthorization();

		app.UseSwagger();
		app.UseAbpSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API");

			var configuration = context.GetConfiguration();
			options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
			options.OAuthScopes("Blog");
		});

		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
		app.UseUnitOfWork();
		app.UseConfiguredEndpoints();
	}
}