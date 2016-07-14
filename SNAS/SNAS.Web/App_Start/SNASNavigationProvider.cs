using Abp.Application.Navigation;
using Abp.Localization;

using SNAS.Core.Authorization;

namespace SNAS.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class SNASNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("首页"),
                        url: "home",
                        icon: "icon  icon-home "
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Softs",
                        L("基础资料"),
                        icon: "fa fa-registered ",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        ).
                        AddItem(
                            new MenuItemDefinition(
                                "Soft",
                                L("软件列表"),
                                url: "soft"
                                ))
                ).AddItem(
                    new MenuItemDefinition(
                        "Softusers",
                        L("业务数据"),
                        icon: "icon icon-users ",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        ).
                        AddItem(
                            new MenuItemDefinition(
                                "softuser",
                                L("用户列表"),
                                url: "softuser"
                                )).
                        AddItem(
                            new MenuItemDefinition(
                                "softuserlicense",
                                L("授权数据"),
                                url: "softuserlicense"
                                )).
                        AddItem(
                            new MenuItemDefinition(
                                "Finance",
                                L("财务明细"),
                                url: "finance"
                                )).
                        AddItem(
                            new MenuItemDefinition(
                                "Softuserlogin",
                                L("登录记录"),
                                url: "softuserlogin"
                                ))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SNASConsts.LocalizationSourceName);
        }
    }
}
