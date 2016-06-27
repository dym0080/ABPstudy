using Abp.Web.Mvc.Views;

namespace MyEventCloud.Web.Views
{
    public abstract class MyEventCloudWebViewPageBase : MyEventCloudWebViewPageBase<dynamic>
    {

    }

    public abstract class MyEventCloudWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MyEventCloudWebViewPageBase()
        {
            LocalizationSourceName = MyEventCloudConsts.LocalizationSourceName;
        }
    }
}