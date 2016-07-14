using System.Collections.Generic;
using System.Web.Optimization;

namespace SNAS.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();


            #region 登录页面

            bundles.Add(
                         new StyleBundle("~/Bundles/App/login/css")
                             .Include(
                                 "~/Assets/Theme/layouts/layout4/css/layout.css",
                                 "~/Assets/Theme/layouts/layout4/css/themes/default.css",
                                 "~/Assets/Theme/global/plugins/font-awesome/css/font-awesome.min.css",
                                 "~/Assets/Theme/global/plugins/simple-line-icons/simple-line-icons.min.css",
                                 "~/Assets/Theme/global/plugins/bootstrap/css/bootstrap.min.css",
                                 "~/Assets/Theme/global/plugins/uniform/css/uniform.default.css",
                                 "~/Assets/Theme/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                                 "~/Assets/Theme/global/plugins/select2/css/select2.min.css",
                                 "~/Assets/Theme/global/plugins/select2/css/select2-bootstrap.min.css",
                                 "~/Assets/Theme/global/css/components-md.min.css",
                                 "~/Assets/Theme/global/css/plugins-md.min.css",
                                 "~/Assets/Theme/layouts/layout4/css/login.min.css"
                             )
                         );

            bundles.Add(
              new ScriptBundle("~/Bundles/App/login/js")
                  .Include(
                      /* ui库所需脚本 */
                      "~/Assets/Theme/global/plugins/jquery.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Assets/Theme/global/plugins/js.cookie.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                      "~/Assets/Theme/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Assets/Theme/global/plugins/jquery.blockui.min.js",
                      "~/Assets/Theme/global/plugins/uniform/jquery.uniform.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Assets/Theme/global/plugins/jquery-validation/js/jquery.validate.min.js",
                      "~/Assets/Theme/global/plugins/jquery-validation/js/additional-methods.min.js",
                      "~/Assets/Theme/global/plugins/select2/js/select2.full.min.js",
                      "~/Assets/Theme/global/plugins/sweetalert/sweet-alert.min.js",
                      "~/Assets/Theme/global/plugins/toastr.js",
                      "~/Assets/Theme/global/scripts/app.min.js",

                       /* abp所需脚本 */
                       "~/Assets/Framework/scripts/abp.js",
                       "~/Assets/Framework/scripts/libs/abp.blockUI.js",
                       "~/Assets/Framework/scripts/libs/abp.jquery.js",
                       "~/Assets/Framework/scripts/libs/abp.blockUI.js",
                       "~/Assets/Framework/scripts/libs/abp.spin.js",
                       "~/Assets/Framework/scripts/libs/abp.sweet-alert.js",
                       "~/Assets/Framework/scripts/libs/abp.toastr.js",

                      /* 登录业务脚本 */
                      "~/Views/Account/login.js"
                  )
              );

            #endregion



            bundles.Add(
                         new StyleBundle("~/Bundles/App/main/css")
                             .Include(                                 
                                 "~/Assets/Theme/global/plugins/font-awesome/css/font-awesome.min.css",
                                 "~/Assets/Theme/global/plugins/simple-line-icons/simple-line-icons.min.css",
                                 "~/Assets/Theme/global/plugins/bootstrap/css/bootstrap.min.css",
                                 "~/Assets/Theme/global/plugins/uniform/css/uniform.default.css",
                                 "~/Assets/Theme/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                                 "~/Assets/Theme/global/plugins/bootstrap-toastr/toastr.css",
                                 "~/Assets/Theme/global/css/components-md.min.css",
                                 "~/Assets/Theme/global/css/plugins-md.min.css",
                                 "~/Assets/Theme/global/plugins/select2/css/select2.min.css",
                                 "~/Assets/Theme/global/plugins/select2/css/select2-bootstrap.min.css",
                                 "~/Assets/Theme/global/plugins/bootstrap-select/css/bootstrap-select.min.css",
                                 "~/Assets/Theme/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                                 "~/Assets/Theme/global/plugins/sweetalert/sweet-alert.css",
                                 "~/Assets/Theme/layouts/layout4/css/layout.css",
                                 "~/Assets/Theme/layouts/layout4/css/themes/light.css",
                                 "~/Assets/Theme/layouts/layout4/css/custom.css"
                             )
                         );


            bundles.Add(
              new ScriptBundle("~/Bundles/App/main/js")
                  .Include(
                      /* ui库所需脚本 */
                      "~/Assets/Theme/global/plugins/jquery.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Assets/Theme/global/plugins/js.cookie.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                      "~/Assets/Theme/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Assets/Theme/global/plugins/jquery.blockui.min.js",
                      "~/Assets/Theme/global/plugins/uniform/jquery.uniform.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Assets/Theme/global/plugins/jquery-validation/js/jquery.validate.min.js",
                      "~/Assets/Theme/global/plugins/jquery-validation/js/additional-methods.min.js",
                      "~/Assets/Theme/global/plugins/select2/js/select2.full.min.js",
                      "~/Assets/Theme/global/plugins/sweetalert/sweet-alert.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-toastr/toastr.js",
                      "~/Assets/Theme/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                      "~/Assets/Theme/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                      "~/Assets/Theme/global/scripts/app.min.js",
                      "~/Assets/Theme/layouts/layout4/scripts/layout.js",
                      "~/Assets/Theme/layouts/layout4/scripts/demo.js",
                      "~/Assets/Theme/global/scripts/quick-sidebar.min.js",

                      /* abp所需脚本 */
                      "~/Assets/Framework/scripts/libs/angularjs/angular.js",
                      "~/Assets/Framework/scripts/libs/angularjs/angular-animate.js",
                      "~/Assets/Framework/scripts/libs/angularjs/angular-sanitize.js",
                      "~/Assets/Framework/scripts/libs/angularjs/angular-ui-router.js",
                      "~/Assets/Framework/scripts/libs/angularjs/ui-bootstrap.js",
                      "~/Assets/Framework/scripts/libs/angularjs/ui-bootstrap-tpls.js",
                      "~/Assets/Framework/scripts/libs/angularjs/ui-utils.js",
                      "~/Assets/Framework/scripts/libs/angularjs/ui-utils-ieshiv.js",

                      "~/Assets/Framework/scripts/milo.search.js",
                      "~/Assets/Framework/scripts/milo.pagination.js",
                      "~/Assets/Framework/scripts/abp.js",
                      "~/Assets/Framework/scripts/libs/angularjs/abp.ng.js",
                       "~/Assets/Framework/scripts/libs/abp.blockUI.js",
                       "~/Assets/Framework/scripts/libs/abp.jquery.js",
                       "~/Assets/Framework/scripts/libs/abp.blockUI.js",
                       "~/Assets/Framework/scripts/libs/abp.spin.js",
                       "~/Assets/Framework/scripts/libs/abp.sweet-alert.js",
                       "~/Assets/Framework/scripts/libs/abp.toastr.js",
                       "~/Assets/Framework/scripts/libs/echarts.common.min.js",
                       "~/Assets/Framework/scripts/milo.echart.line.js",
                       "~/App/app.js",
                       "~/App/services/appSession.js",
                       "~/Assets/Theme/global/scripts/app.plugin.js",

                      /* 登录业务脚本 */
                      "~/Views/Account/login.js"
                  )
              );

            //页面js
            bundles.Add(
              new ScriptBundle("~/Bundles/App/view/js")                                     
                   .IncludeDirectory("~/App/views", "*.js", true)
              );

            //ie fix

            bundles.Add(
             new ScriptBundle("~/Bundles/App/iefix")
                 .Include(
                     "~/Assets/global/plugins/respond.min.js",
                     "~/Assets/global/plugins/excanvas.min.js"
                 )
             );

            //#region 公用css

            ////全局资源
            //bundles.Add(
            //    new StyleBundle("~/Bundles/App/global/css")
            //        .Include(
            //            "~/Assets/plugins/font-awesome/css/font-awesome.min.css",
            //            "~/Assets/plugins/simple-line-icons/simple-line-icons.min.css",
            //            "~/Assets/plugins/bootstrap/css/bootstrap.min.css",
            //            "~/Assets/plugins/uniform/css/uniform.default.css",
            //            "~/Content/toastr.min.css",
            //            "~/Scripts/sweetalert/sweet-alert.css",
            //            "~/Content/flags/famfamfam-flags.css"

            //        )
            //    );

            ////主题样式
            //bundles.Add(
            //  new StyleBundle("~/Bundles/App/theme/css")
            //      .Include(
            //          "~/Assets/css/components-md.css",
            //          "~/Assets/css/plugins-md.css"

            //      )
            //  );

            //#endregion

            //#region 公用js

            ////IE浏览器兼容补丁
            //bundles.Add(
            //  new ScriptBundle("~/Bundles/App/iefix/js")
            //      .Include(
            //          "~/Assets/plugins/respond.min.js",
            //          "~/Assets/plugins/excanvas.min.js"
            //      )
            //  );

            ////核心js
            //bundles.Add(
            // new ScriptBundle("~/Bundles/App/core/js")
            //     .Include(
            //         "~/Abp/Framework/scripts/utils/ie10fix.js",
            //         "~/Scripts/json2.min.js",
            //         "~/Scripts/modernizr-2.8.3.js",
            //         "~/Scripts/jquery-2.1.4.min.js",
            //         "~/Scripts/jquery-ui-1.11.4.min.js",

            //         "~/Assets/plugins/jquery-migrate.min.js",
            //         "~/Assets/plugins/bootstrap/js/bootstrap.min.js",
            //         "~/Assets/plugins/jquery.blockui.min.js",
            //         "~/Assets/plugins/uniform/jquery.uniform.min.js",
            //         "~/Assets/plugins/jquery.cokie.min.js",
            //         "~/Assets/plugins/jquery-validation/js/jquery.validate.min.js",

            //        "~/Scripts/moment-with-locales.min.js",                        
            //            "~/Scripts/toastr.min.js",
            //            "~/Scripts/sweetalert/sweet-alert.min.js",
            //            "~/Scripts/others/spinjs/spin.js",
            //            "~/Scripts/others/spinjs/jquery.spin.js",

            //            "~/Scripts/angular.min.js",
            //            "~/Scripts/angular-animate.min.js",
            //            "~/Scripts/angular-sanitize.min.js",
            //            "~/Scripts/angular-ui-router.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
            //            "~/Scripts/angular-ui/ui-utils.min.js",

            //            "~/Abp/Framework/scripts/tm.pagination.js",
            //            "~/Abp/Framework/scripts/abp.js",
            //            "~/Abp/Framework/scripts/libs/abp.jquery.js",
            //            "~/Abp/Framework/scripts/libs/abp.toastr.js",
            //            "~/Abp/Framework/scripts/libs/abp.blockUI.js",
            //            "~/Abp/Framework/scripts/libs/abp.spin.js",
            //            "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
            //            "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
            //     )
            // );

            ////主题js
            //bundles.Add(
            //  new ScriptBundle("~/Bundles/App/theme/js")
            //      .Include(
            //          "~/Assets/scripts/metronic.js"

            //      )
            //       .IncludeDirectory("~/Common/Scripts", "*.js", true)
            //        .IncludeDirectory("~/App/Main", "*.js", true)
            //  );

            foreach (var item in bundles)
            {
                item.Orderer = new AsIsBundleOrderer();
            }

            //#endregion

            //#region 页面单独样式

            //#region 登录
            //bundles.Add(
            //         new StyleBundle("~/Bundles/App/login/css")
            //             .Include(
            //                 "~/Views/Account/login.css",
            //                 "~/Assets/layout/css/layout.css",
            //                 "~/Assets/layout/css/themes/default.css",
            //                 "~/Assets/layout/css/custom.css"
            //             )
            //         );

            //bundles.Add(
            //  new ScriptBundle("~/Bundles/App/login/js")
            //      .Include(
            //          "~/Views/Account/login.js",
            //          "~/Assets/layout/scripts/layout.js",
            //          "~/Assets/layout/scripts/demo.js"
            //      )
            //  );
            //#endregion

            //#region 登录
            //bundles.Add(
            //         new StyleBundle("~/Bundles/App/home/css")
            //             .Include(                             
            //                 "~/Assets/layout4/css/layout.css",
            //                 "~/Assets/layout4/css/themes/light.css",
            //                 "~/Assets/layout4/css/custom.css"
            //             )
            //         );

            //bundles.Add(
            //  new ScriptBundle("~/Bundles/App/home/js")
            //      .Include(                      
            //          "~/Assets/layout4/scripts/layout.js",
            //          "~/Assets/layout4/scripts/demo.js"
            //      )
            //  );
            //#endregion

            //#endregion


            ////VENDOR RESOURCES

            ////~/Bundles/App/vendor/css
            //bundles.Add(
            //    new StyleBundle("~/Bundles/App/vendor/css")
            //        .Include(
            //            "~/Content/themes/base/all.css",
            //            "~/Content/bootstrap-cosmo.min.css",
            //            "~/Content/toastr.min.css",
            //            "~/Scripts/sweetalert/sweet-alert.css",
            //            "~/Content/flags/famfamfam-flags.css",
            //            "~/Content/font-awesome.min.css"
            //        )
            //    );

            ////~/Bundles/App/vendor/js
            //bundles.Add(
            //    new ScriptBundle("~/Bundles/App/vendor/js")
            //        .Include(
            //            "~/Abp/Framework/scripts/utils/ie10fix.js",
            //            "~/Scripts/json2.min.js",

            //            "~/Scripts/modernizr-2.8.3.js",

            //            "~/Scripts/jquery-2.1.4.min.js",
            //            "~/Scripts/jquery-ui-1.11.4.min.js",

            //            "~/Scripts/bootstrap.min.js",

            //            "~/Scripts/moment-with-locales.min.js",
            //            "~/Scripts/jquery.blockUI.js",
            //            "~/Scripts/toastr.min.js",
            //            "~/Scripts/sweetalert/sweet-alert.min.js",
            //            "~/Scripts/others/spinjs/spin.js",
            //            "~/Scripts/others/spinjs/jquery.spin.js",

            //            "~/Scripts/angular.min.js",
            //            "~/Scripts/angular-animate.min.js",
            //            "~/Scripts/angular-sanitize.min.js",
            //            "~/Scripts/angular-ui-router.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap.min.js",
            //            "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
            //            "~/Scripts/angular-ui/ui-utils.min.js",

            //            "~/Abp/Framework/scripts/tm.pagination.js",
            //            "~/Abp/Framework/scripts/abp.js",
            //            "~/Abp/Framework/scripts/libs/abp.jquery.js",
            //            "~/Abp/Framework/scripts/libs/abp.toastr.js",
            //            "~/Abp/Framework/scripts/libs/abp.blockUI.js",
            //            "~/Abp/Framework/scripts/libs/abp.spin.js",
            //            "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
            //            "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
            //        )
            //    );

            ////APPLICATION RESOURCES

            ////~/Bundles/App/Main/css
            //bundles.Add(
            //    new StyleBundle("~/Bundles/App/Main/css")
            //        .IncludeDirectory("~/App/Main", "*.css", true)
            //    );

            ////~/Bundles/App/Main/js
            //bundles.Add(
            //    new ScriptBundle("~/Bundles/App/Main/js")
            //        .IncludeDirectory("~/Common/Scripts", "*.js", true)
            //        .IncludeDirectory("~/App/Main", "*.js", true)
            //    );
        }
    }

    /// <summary>
    /// 脚本顺序控制，先录入时的顺序(默认是按字母排序)
    /// </summary>
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

}