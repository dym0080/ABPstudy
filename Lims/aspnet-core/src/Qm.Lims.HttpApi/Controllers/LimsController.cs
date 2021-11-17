using Qm.Lims.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Qm.Lims.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class LimsController : AbpController
    {
        protected LimsController()
        {
            LocalizationResource = typeof(LimsResource);
        }
    }
}