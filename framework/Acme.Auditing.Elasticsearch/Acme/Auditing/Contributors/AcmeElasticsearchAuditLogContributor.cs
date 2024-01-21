using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acme.Auditing.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Acme.Auditing.Contributors
{
	public class AcmeElasticsearchAuditLogContributor : AuditLogContributor, ITransientDependency
	{
		public override void PreContribute(AuditLogContributionContext context)
		{
			var options = context.ServiceProvider.GetRequiredService<IOptions<AcmeAuditingElasticsearchOptions>>().Value;
			if (!options.Enable)
			{
				return;
			}

			var httpContext = context.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
			if (httpContext == null)
			{
				return;
			}

			var httpHeader = httpContext.Request.Headers.Select(x => $"{x.Key}:{x.Value}").JoinAsString("\r\n");
			context.AuditInfo.ExtraProperties.Add("request_header", httpHeader);

			if (httpContext.Request.ContentType?.Contains("application/json") == true)
			{
				httpContext.Request.EnableBuffering();
				var httpBody = AsyncHelper.RunSync(() => new StreamReader(httpContext.Request.Body).ReadToEndAsync());
				httpContext.Request.Body.Position = 0;
				context.AuditInfo.ExtraProperties.Add("request_body", httpBody);
			}
		}
	}
}