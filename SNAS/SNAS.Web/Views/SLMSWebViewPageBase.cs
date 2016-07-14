using Abp.Web.Mvc.Views;

namespace SNAS.Web.Views
{
    public abstract class SNASWebViewPageBase : SNASWebViewPageBase<dynamic>
    {

    }

    public abstract class SNASWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SNASWebViewPageBase()
        {
            LocalizationSourceName = SNASConsts.LocalizationSourceName;
        }
    }
}