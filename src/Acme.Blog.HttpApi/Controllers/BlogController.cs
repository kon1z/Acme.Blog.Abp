using Acme.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BlogController : AbpControllerBase
{
    protected BlogController()
    {
        LocalizationResource = typeof(BlogResource);
    }
}