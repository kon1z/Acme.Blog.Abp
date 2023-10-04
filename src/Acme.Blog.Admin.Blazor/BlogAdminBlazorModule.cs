using Acme.Localization;
using Acme.MultiTenancy;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Acme.Blog.Admin.Blazor;

[DependsOn(
    typeof(BlogHttpApiClientModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAutoMapperModule)
)]
public class BlogAdminBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(BlogResource),
                typeof(BlogDomainSharedModule).Assembly,
                typeof(BlogApplicationContractsModule).Assembly,
                typeof(BlogAdminBlazorModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureUrls(configuration);
        ConfigureMultiTenancy();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureBlazor(context);
        ConfigureAntBlazor(context);
    }

    private void ConfigureAntBlazor(ServiceConfigurationContext context)
    {
        context.Services.AddAntDesign();
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["BlogAdmin"].RootUrl = configuration["App:SelfUrl"];
            options.Applications["BlogHost"].RootUrl = configuration["RemoteServices:Default:BaseUrl"];
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BlogDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Acme.Blog.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<BlogApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Acme.Blog.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<BlogAdminBlazorModule>(hostingEnvironment.ContentRootPath);
            });
    }

    private void ConfigureBlazor(ServiceConfigurationContext context)
    {
        context.Services.AddRazorPages();
        context.Services.AddServerSideBlazor();
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<BlogAdminBlazorModule>(); });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseAbpRequestLocalization();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAbpSerilogEnrichers();
    }
}