﻿using Elastic.Clients.Elasticsearch;

namespace Acme.Auditing.Elasticsearch
{
	public class AcmeAuditingElasticsearchOptions
	{
		public bool Enable { get; set; }
		public string? Node { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }

		public string Environment { get; set; } = null!;

		public string RequestIndexName { get; set; } = null!;
	}
}
