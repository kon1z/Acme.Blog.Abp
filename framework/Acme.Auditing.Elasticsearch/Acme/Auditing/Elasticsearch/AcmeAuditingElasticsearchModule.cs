using Acme.Auditing.Contributors;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

			context.Services.Configure<AcmeAuditingElasticsearchOptions>(options =>
			{
				options.Enable = configuration.GetValue<bool>("Elasticsearch:Enable");
				// TODO SingleNode or CloudNode, this property is not work now.
				options.Node = configuration.GetSection("Elasticsearch:Node").Get<string[]>();

				options.Username = configuration["Elasticsearch:Username"];
				options.Password = configuration["Elasticsearch:Password"];

				if (!options.Username.IsNullOrEmpty()
					&& !options.Password.IsNullOrEmpty())
				{
					options.Authentication(new BasicAuthentication(options.Username, options.Password));
				}

				options.RequestIndexName = configuration["Elasticsearch:RequestIndexName"] ?? "request";
			});

			// TODO Link Test

			context.Services.Configure<AbpAuditingOptions>(options =>
			{
				options.Contributors.Add(new AcmeElasticsearchAuditLogContributor());
			});
		}
	}
}
