﻿using Abp.Application.Navigation;
using Abp.Localization;

namespace ABPDemoNoZero.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class ABPDemoNoZeroNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.MainMenu
            //    .AddItem(
            //        new MenuItemDefinition(
            //            "Home",
            //            new LocalizableString("HomePage", ABPDemoNoZeroConsts.LocalizationSourceName),
            //            url: "#/",
            //            icon: "fa fa-home"
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "About",
            //            new LocalizableString("About", ABPDemoNoZeroConsts.LocalizationSourceName),
            //            url: "#/about",
            //            icon: "fa fa-info"
            //            )
            //    );

            context.Manager.MainMenu
               .AddItem(
                   new MenuItemDefinition(
                       "TaskList",
                       new LocalizableString("TaskList", ABPDemoNoZeroConsts.LocalizationSourceName),
                       url: "#/",
                       icon: "fa fa-tasks"
                       )
               ).AddItem(
                   new MenuItemDefinition(
                       "NewTask",
                       new LocalizableString("NewTask", ABPDemoNoZeroConsts.LocalizationSourceName),
                       url: "#/new",
                       icon: "fa fa-asterisk"
                       )
               );
        }
    }
}
