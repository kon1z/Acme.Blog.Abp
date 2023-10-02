using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme;

[Dependency(ReplaceServices = true)]
public class BlogBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Blog";
}