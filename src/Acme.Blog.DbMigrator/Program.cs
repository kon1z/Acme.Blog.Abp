using Acme.Blog.DbMigrator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
	.MinimumLevel.Override("Acme.Blog", LogEventLevel.Debug)
#else
    .MinimumLevel.Override("Acme.Blog", LogEventLevel.Information)
#endif
	.Enrich.FromLogContext()
	.WriteTo.Async(c => c.File("Logs/logs.txt"))
	.WriteTo.Async(c => c.Console())
	.CreateLogger();

await Host.CreateDefaultBuilder(args)
	.AddAppSettingsSecretsJson()
	.ConfigureLogging((context, logging) => logging.ClearProviders())
	.ConfigureServices((hostContext, services) =>
	{
		services.AddHostedService<DbMigratorHostedService>();
	})
	.RunConsoleAsync();