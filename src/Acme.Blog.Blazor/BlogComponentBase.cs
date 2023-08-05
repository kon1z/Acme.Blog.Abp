using Acme.Blog.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Acme.Blog.Blazor;

public abstract class BlogComponentBase : AbpComponentBase
{
    protected BlogComponentBase()
    {
        LocalizationResource = typeof(BlogResource);
    }
}
