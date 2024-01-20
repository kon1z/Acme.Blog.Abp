using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Acme.Auditing.Contributors
{
	public class AcmeElasticsearchAuditLogContributor : AuditLogContributor, ITransientDependency
	{
		public override void PreContribute(AuditLogContributionContext context)
		{
			base.PreContribute(context);
		}
	}
}
