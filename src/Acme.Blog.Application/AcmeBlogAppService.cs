using Acme.Localization;
using Volo.Abp.Application.Services;

namespace Acme;

/* Inherit your application services from this class.
 */
public abstract class AcmeBlogAppService : ApplicationService
{
	protected AcmeBlogAppService()
	{
		LocalizationResource = typeof(BlogResource);
	}
}