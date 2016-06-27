using Abp.Application.Services;

namespace ABPDemoNoZero
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ABPDemoNoZeroAppServiceBase : ApplicationService
    {
        protected ABPDemoNoZeroAppServiceBase()
        {
            LocalizationSourceName = ABPDemoNoZeroConsts.LocalizationSourceName;
        }
    }
}