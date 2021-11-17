using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Qm.Lims.Web
{
    [Dependency(ReplaceServices = true)]
    public class LimsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Lims";
    }
}
