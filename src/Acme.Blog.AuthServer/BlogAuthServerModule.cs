using System;
using System.IO;
using System.Linq;
using Acme.Blog.EntityFrameworkCore;
using Acme.Blog.Localization;
using Acme.Blog.MultiTenancy;
using Localization.Resources.AbpUi;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.Security.Claims;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Acme.Blog;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(AbpCachingStackExchangeRedisModule),
	typeof(AbpDistributedLockingModule),
	typeof(AbpAccountWebOpenIddictModule),
	typeof(AbpAccountApplicationModule),
	typeof(AbpAccountHttpApiModule),
	typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
	typeof(BlogEntityFrameworkCoreModule),
	typeof(AbpAspNetCoreSerilogModule)
)]
public class BlogAuthServerModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		PreConfigure<OpenIddictBuilder>(builder =>
		{
			builder.AddValidation(options =>
			{
				options.AddAudiences("Blog");
				options.UseLocalServer();
				options.UseAspNetCore();
			});
		});

		if (!hostingEnvironment.IsDevelopment())
		{
			PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
			{
				options.AddDevelopmentEncryptionAndSigningCertificate = false;
			});

			PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
			{
				serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx",
					"5e4d3811-0bdf-4d4a-9b37-7086290b6d23");
			});
		}
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Get<BlogResource>()
				.AddBaseTypes(
					typeof(AbpUiResource),
					typeof(AccountResource)
				);
		});

		Configure<AbpBundlingOptions>(options =>
		{
			options.StyleBundles.Configure(
				LeptonXLiteThemeBundles.Styles.Global,
				bundle => { bundle.AddFiles("/global-styles.css"); }
			);
		});

		Configure<AbpAuditingOptions>(options =>
		{
			//options.IsEnabledForGetRequests = true;
			options.ApplicationName = "AuthServer";
		});

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainSharedModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Domain.Shared"));
				options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainModule>(
					Path.Combine(hostingEnvironment.ContentRootPath,
						$"..{Path.DirectorySeparatorChar}Acme.Blog.Domain"));
			});
		}

		Configure<AppUrlOptions>(options =>
		{
			options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
			options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ??
			                                     Array.Empty<string>());

			options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
			options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
		});

		Configure<AbpBackgroundJobOptions>(options => { options.IsJobExecutionEnabled = false; });

		Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Blog:"; });

		var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Blog");
		if (!hostingEnvironment.IsDevelopment())
		{
			var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Blog-Protection-Keys");
		}

		context.Services.AddSingleton<IDistributedLockProvider>(sp =>
		{
			var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
		});

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

		context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
		{
			options.IsDynamicClaimsEnabled = true;
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
		app.UseDynamicClaims();
		app.UseAuthorization();

		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
		app.UseConfiguredEndpoints();
	}
}