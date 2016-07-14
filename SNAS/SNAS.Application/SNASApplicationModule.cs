using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using SNAS.Application.AutoMapper;

namespace SNAS.Application
{
    [DependsOn(typeof(SNASCoreModule), typeof(AbpAutoMapperModule))]
    public class SNASApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            AutoMapperWebConfig.Configure();//一次性加载所有映射配
        }
    }
}
