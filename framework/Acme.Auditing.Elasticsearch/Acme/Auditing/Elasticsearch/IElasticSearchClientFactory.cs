using Elastic.Clients.Elasticsearch;

namespace Acme.Auditing.Elasticsearch;

public interface IElasticSearchClientFactory
{
	ElasticsearchClient BuildElasticsearchClient(AcmeAuditingElasticsearchOptions options);
}