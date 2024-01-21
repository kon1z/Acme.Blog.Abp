using Acme.Auditing.Indexes;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Acme.Auditing.Elasticsearch
{
	[ExposeServices(typeof(IAuditingStore))]
	public class AcmeElasticsearchAuditingStore : ElasticsearchClient, IAuditingStore, ITransientDependency
	{
		private AcmeAuditingElasticsearchOptions Options { get; set; }

		public AcmeElasticsearchAuditingStore(IOptions<AcmeAuditingElasticsearchOptions> options) : base(options.Value)
		{
			Options = options.Value;
		}

		public async Task SaveAsync(AuditLogInfo auditInfo)
		{
			if (!Options.Enable)
			{
				return;
			}

			var requestIndex = new RequestElasticsearchIndex(auditInfo.ApplicationName, auditInfo.UserId, auditInfo.UserName,
				auditInfo.TenantId, auditInfo.TenantName, auditInfo.ImpersonatorUserId, auditInfo.ImpersonatorTenantId,
				auditInfo.ImpersonatorUserName, auditInfo.ImpersonatorTenantName, auditInfo.ExecutionTime,
				auditInfo.ExecutionDuration, auditInfo.ClientId, auditInfo.CorrelationId, auditInfo.ClientIpAddress,
				auditInfo.ClientName, auditInfo.BrowserInfo, auditInfo.HttpMethod, auditInfo.HttpStatusCode,
				auditInfo.Url, auditInfo.ExtraProperties["request_header"]?.ToString(),
				auditInfo.ExtraProperties["request_body"]?.ToString());
			await IndexAsync(new IndexRequestDescriptor<RequestElasticsearchIndex>(requestIndex)
					.Index($"{Options.RequestIndexName}-{Options.Environment}".ToLower()));
		}
	}
}
