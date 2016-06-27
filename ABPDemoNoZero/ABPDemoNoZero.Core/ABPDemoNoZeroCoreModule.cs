using System.Reflection;
using Abp.Modules;

namespace ABPDemoNoZero
{
    public class ABPDemoNoZeroCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
