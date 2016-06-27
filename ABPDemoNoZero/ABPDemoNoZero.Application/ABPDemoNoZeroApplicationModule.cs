using System.Reflection;
using Abp.Modules;

namespace ABPDemoNoZero
{
    [DependsOn(typeof(ABPDemoNoZeroCoreModule))]
    public class ABPDemoNoZeroApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //We must declare mappings to be able to use AutoMapper
            DtoMappings.Map();
        }
    }
}
