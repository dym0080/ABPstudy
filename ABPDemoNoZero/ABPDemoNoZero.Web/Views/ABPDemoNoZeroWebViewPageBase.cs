using Abp.Web.Mvc.Views;

namespace ABPDemoNoZero.Web.Views
{
    public abstract class ABPDemoNoZeroWebViewPageBase : ABPDemoNoZeroWebViewPageBase<dynamic>
    {

    }

    public abstract class ABPDemoNoZeroWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ABPDemoNoZeroWebViewPageBase()
        {
            LocalizationSourceName = ABPDemoNoZeroConsts.LocalizationSourceName;
        }
    }
}