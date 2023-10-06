using Acme.EntityFrameworkCore;
using Acme.Localization;
using Acme.MultiTenancy;
using Localization.Resources.AbpUi;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Acme.Blog
{
	[DependsOn(
		typeof(AbpAutofacModule),
		typeof(BlogEntityFrameworkCoreModule),
		typeof(AbpAutofacModule),
		typeof(AbpCachingStackExchangeRedisModule),
		typeof(AbpDistributedLockingModule),
		typeof(AbpAccountWebOpenIddictModule),
		typeof(AbpAccountApplicationModule),
		typeof(AbpAccountHttpApiModule),
		typeof(AbpAspNetCoreSerilogModule)
		)]
	public class BlogAuthServerBlazorModule : AbpModule
	{
		public override void PreConfigureServices(ServiceConfigurationContext context)
		{
			PreConfigure<OpenIddictBuilder>(builder =>
			{
				builder.AddValidation(options =>
				{
					options.AddAudiences("Acme.Blog.AuthServer");
					options.UseLocalServer();
					options.UseAspNetCore();
				});
			});
		}

		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			var hostEnvironment = context.Services.GetHostingEnvironment();
			var configuration = context.Services.GetConfiguration();

			ConfigureLocation();
			ConfigureAuditing();
			ConfigureVirtualFileSystem(hostEnvironment);
			ConfigureBackgroundJob();
			ConfigureCors(context, configuration);
			ConfigureDistributedCacheAndDistributedLock(context, hostEnvironment, configuration);
			ConfigureTheme(context);
		}

		private void ConfigureTheme(ServiceConfigurationContext context)
		{
			Configure<AbpThemingOptions>(options =>
			{
			});
		}

		private void ConfigureLocation()
		{
			Configure<AbpLocalizationOptions>(options =>
			{
				options.Resources
					.Get<BlogResource>()
					.AddBaseTypes(
						typeof(AbpUiResource)
					);
			});
		}

		private void ConfigureAuditing()
		{
			Configure<AbpAuditingOptions>(options =>
			{
				//options.IsEnabledForGetRequests = true;
				options.ApplicationName = "AuthServer";
			});
		}

		private void ConfigureVirtualFileSystem(IWebHostEnvironment hostEnvironment)
		{
			if (hostEnvironment.IsDevelopment())
			{
				Configure<AbpVirtualFileSystemOptions>(options =>
				{
					options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainSharedModule>(Path.Combine(hostEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.Blog.Domain.Shared"));
					options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainModule>(Path.Combine(hostEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.Blog.Domain"));
				});
			}
		}

		private void ConfigureBackgroundJob()
		{
			Configure<AbpBackgroundJobOptions>(options =>
			{
				options.IsJobExecutionEnabled = false;
			});
		}

		private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
		{
			context.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder
						.WithOrigins(
							configuration["App:CorsOrigins"]?
								.Split(",", StringSplitOptions.RemoveEmptyEntries)
								.Select(o => o.RemovePostFix("/"))
								.ToArray() ?? Array.Empty<string>()
						)
						.WithAbpExposedHeaders()
						.SetIsOriginAllowedToAllowWildcardSubdomains()
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials();
				});
			});
		}

		private void ConfigureDistributedCacheAndDistributedLock(ServiceConfigurationContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
		{
			Configure<AbpDistributedCacheOptions>(options =>
			{
				options.KeyPrefix = "AuthServer:";
			});

			var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("AuthServer");
			if (!hostEnvironment.IsDevelopment())
			{
				var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
				dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "AuthServer-Protection-Keys");
			}

			context.Services.AddSingleton<IDistributedLockProvider>(sp =>
			{
				var connection = ConnectionMultiplexer
					.Connect(configuration["Redis:Configuration"]!);
				return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
			});
		}

		public override void OnApplicationInitialization(ApplicationInitializationContext context)
		{
			var app = context.GetApplicationBuilder();
			var env = context.GetEnvironment();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAbpRequestLocalization();

			if (!env.IsDevelopment())
			{
				app.UseErrorPage();
			}

			app.UseCorrelationId();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCors();
			app.UseAuthentication();
			app.UseAbpOpenIddictValidation();

			if (MultiTenancyConsts.IsEnabled)
			{
				app.UseMultiTenancy();
			}

			app.UseUnitOfWork();
			app.UseAuthorization();
			app.UseAuditing();
			app.UseAbpSerilogEnrichers();
			app.UseConfiguredEndpoints();
		}
	}
}
