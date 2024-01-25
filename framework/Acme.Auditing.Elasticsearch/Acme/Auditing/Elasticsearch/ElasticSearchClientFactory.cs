using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using System;
using Volo.Abp.DependencyInjection;

namespace Acme.Auditing.Elasticsearch;

public class ElasticSearchClientFactory : IElasticSearchClientFactory, ITransientDependency
{
	public ElasticsearchClient BuildElasticsearchClient(AcmeAuditingElasticsearchOptions options)
	{
		// TODO Need more link types, example SingleNode, CloudNode
		// TODO Need more authentication types, example Token
		// TODO More parameters need to be checked and more exception to be thrown

		var elasticsearchClientSettings = new ElasticsearchClientSettings(new Uri(options.Node ?? "http://localhost:9200"));
		if (!options.Username.IsNullOrEmpty() && !options.Password.IsNullOrEmpty())
		{
			elasticsearchClientSettings.Authentication(new BasicAuthentication(options.Username, options.Password));
		}

		return new ElasticsearchClient(elasticsearchClientSettings);
	}
}