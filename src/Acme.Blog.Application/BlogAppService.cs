using System;
using System.Collections.Generic;
using System.Text;
using Acme.Blog.Localization;
using Volo.Abp.Application.Services;

namespace Acme.Blog;

/* Inherit your application services from this class.
 */
public abstract class BlogAppService : ApplicationService
{
    protected BlogAppService()
    {
        LocalizationResource = typeof(BlogResource);
    }
}
