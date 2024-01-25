using Acme.Auditing.Indexes;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Acme.Auditing.Elasticsearch
{
	[ExposeServices(typeof(IAuditingStore))]
	public class AcmeElasticsearchAuditingStore : IAuditingStore, ITransientDependency
	{
		private AcmeAuditingElasticsearchOptions Options { get; }
		private ElasticsearchClient ElasticsearchClient { get; }


		public AcmeElasticsearchAuditingStore(
			IOptions<AcmeAuditingElasticsearchOptions> options,
			ElasticsearchClient elasticsearchClient)
		{
			ElasticsearchClient = elasticsearchClient;
			Options = options.Value;
		}

		public async Task SaveAsync(AuditLogInfo auditInfo)
		{
			if (!Options.Enable)
			{
				return;
			}

			var requestHeader = auditInfo.ExtraProperties.GetValueOrDefault("request_header")?.ToString();
			var requestBody = auditInfo.ExtraProperties.GetValueOrDefault("request_body")?.ToString();

			var requestIndex = new RequestElasticsearchIndex(auditInfo.ApplicationName, auditInfo.UserId, auditInfo.UserName,
				auditInfo.TenantId, auditInfo.TenantName, auditInfo.ImpersonatorUserId, auditInfo.ImpersonatorTenantId,
				auditInfo.ImpersonatorUserName, auditInfo.ImpersonatorTenantName, auditInfo.ExecutionTime,
				auditInfo.ExecutionDuration, auditInfo.ClientId, auditInfo.CorrelationId, auditInfo.ClientIpAddress,
				auditInfo.ClientName, auditInfo.BrowserInfo, auditInfo.HttpMethod, auditInfo.HttpStatusCode,
				auditInfo.Url, requestHeader, requestBody);
			await ElasticsearchClient.IndexAsync(
				new IndexRequestDescriptor<RequestElasticsearchIndex>(requestIndex)
					.Index(Options.RequestIndexName));
		}
	}
}
