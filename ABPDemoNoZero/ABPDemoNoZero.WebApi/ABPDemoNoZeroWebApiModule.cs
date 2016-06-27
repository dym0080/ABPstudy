using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace ABPDemoNoZero
{
    [DependsOn(typeof(AbpWebApiModule), typeof(ABPDemoNoZeroApplicationModule))]
    public class ABPDemoNoZeroWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(ABPDemoNoZeroApplicationModule).Assembly, "tasksystem")
                .Build();
        }
    }
}
