using Qm.Lims.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Qm.Lims.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class LimsPageModel : AbpPageModel
    {
        protected LimsPageModel()
        {
            LocalizationResourceType = typeof(LimsResource);
        }
    }
}