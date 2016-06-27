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
        }
    }
}
