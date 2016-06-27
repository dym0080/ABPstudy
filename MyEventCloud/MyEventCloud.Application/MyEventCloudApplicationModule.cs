using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace MyEventCloud
{
    [DependsOn(typeof(MyEventCloudCoreModule), typeof(AbpAutoMapperModule))]
    public class MyEventCloudApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
