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
		}
	}
}
