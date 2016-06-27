using Abp.Web.Mvc.Controllers;

namespace ABPDemoNoZero.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class ABPDemoNoZeroControllerBase : AbpController
    {
        protected ABPDemoNoZeroControllerBase()
        {
            LocalizationSourceName = ABPDemoNoZeroConsts.LocalizationSourceName;
        }
    }
}