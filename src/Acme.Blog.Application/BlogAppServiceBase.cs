using Acme.Blog.Localization;
using Volo.Abp.Application.Services;

namespace Acme.Blog;

/* Inherit your application services from this class.
 */
public abstract class BlogAppServiceBase : ApplicationService
{
	protected BlogAppServiceBase()
	{
		LocalizationResource = typeof(BlogResource);
	}
}