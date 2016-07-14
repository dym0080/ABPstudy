using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using SNAS.Application;
using SNAS.WebApi.Api.Controllers;
using Swashbuckle.Application;
using Swashbuckle.Swagger;

namespace SNAS.WebApi.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(SNASApplicationModule))]
    public class SNASWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(SNASApplicationModule).Assembly, "app")
                .Build();

            ConfigureSwaggerUi();

        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
            name: "SoftController_Login",
            routeTemplate: "api/Soft/Login",
            defaults: new { controller = "Soft", action = "Login" }
            );

            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
                name: "SoftController_Register",
                routeTemplate: "api/Soft/Register",
                defaults: new { controller = "Soft", action = "Register" }
                );

            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
                name: "SoftController_Authorize",
                routeTemplate: "api/Soft/Authorize",
                defaults: new { controller = "Soft", action = "Authorize" }
                );
            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
                name: "SoftController_ChangePassword",
                routeTemplate: "api/Soft/ChangePassword",
                defaults: new { controller = "Soft", action = "ChangePassword" }
                );

            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
               name: "SoftController_ChangeMcode",
               routeTemplate: "api/Soft/ChangeMcode",
               defaults: new { controller = "Soft", action = "ChangeMcode" }
               );
            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
             name: "SoftController_CheckStatus",
             routeTemplate: "api/Soft/CheckStatus",
             defaults: new { controller = "Soft", action = "CheckStatus" }
             );
            Configuration.Modules.AbpWebApi().HttpConfiguration.Routes.MapHttpRoute(
             name: "SoftController_Logout",
             routeTemplate: "api/Soft/Logout",
             defaults: new { controller = "Soft", action = "Logout" }
             );

            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                               {
                                   c.SingleApiVersion("v1", "webapi接口列表")
                                       .Description("接口明细如下,可以在这里对api进行测试运行");
                                   c.DocumentFilter<SoftDocumentFilter>();
                                   c.IncludeXmlComments(GetXmlCommentsPath());
                                   c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                               })
                .EnableSwaggerUi(c =>
                                 {
                                     c.InjectJavaScript(Assembly.GetExecutingAssembly(), "SNAS.WebApi.Swashbuckle.translator.js");
                                     c.DisableValidator();
                                     c.DocExpansion(DocExpansion.List);
                                 });


        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\SNAS.WebApi.XML", AppDomain.CurrentDomain.BaseDirectory);
        }

        public class SoftDocumentFilter : IDocumentFilter
        {
            public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
            {
                for (int i = apiExplorer.ApiDescriptions.Count - 1; i >= 0; i--)
                {
                    var item = apiExplorer.ApiDescriptions[i];
                    if (item.ActionDescriptor.ControllerDescriptor.ControllerType != typeof(SoftController))
                    {
                        apiExplorer.ApiDescriptions.RemoveAt(i);
                    }
                    else
                    {
                        if (item.RelativePath.StartsWith("api/Soft?"))
                        {
                            apiExplorer.ApiDescriptions.RemoveAt(i);
                        }
                    }
                }
            }
        }

    }
}
