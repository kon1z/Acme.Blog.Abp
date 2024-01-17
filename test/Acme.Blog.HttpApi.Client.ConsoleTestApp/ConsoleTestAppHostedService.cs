using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace Acme.Blog.HttpApi.Client.ConsoleTestApp;

public class ConsoleTestAppHostedService(IConfiguration configuration) : IHostedService
{
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using (var application = await AbpApplicationFactory.CreateAsync<BlogConsoleApiClientModule>(options =>
		       {
			       options.Services.ReplaceConfiguration(configuration);
			       options.UseAutofac();
		       }))
		{
			await application.InitializeAsync();

			var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
			await demo.RunAsync();

			await application.ShutdownAsync();
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}