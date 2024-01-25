using Acme.Auditing.Contributors;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace Acme.Auditing.Elasticsearch
{
	[DependsOn(
		typeof(AbpAspNetCoreModule)
		)]
	public class AcmeAuditingElasticsearchModule : AbpModule
	{
		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			var configuration = context.Services.GetConfiguration();
			var hostingEnvironment = context.Services.GetHostingEnvironment();
			
			context.Services.Configure<AcmeAuditingElasticsearchOptions>(options =>
			{
				options.Enable = configuration.GetValue<bool>("Elasticsearch:Enable");
				options.Node = configuration["Elasticsearch:Node"];

				options.Username = configuration["Elasticsearch:Username"];
				options.Password = configuration["Elasticsearch:Password"];

				options.RequestIndexName = $"request-{hostingEnvironment.EnvironmentName.ToLower()}";
			});

			context.Services.AddTransient<ElasticsearchClient>(sp =>
			{
				var options = sp.GetRequiredService<IOptions<AcmeAuditingElasticsearchOptions>>().Value;
				var factory = sp.GetRequiredService<IElasticSearchClientFactory>();
				return factory.BuildElasticsearchClient(options);
			});

			// TODO Link Test

			context.Services.Configure<AbpAuditingOptions>(options =>
			{
				options.Contributors.Add(new AcmeElasticsearchAuditLogContributor());
			});
		}
	}
}
