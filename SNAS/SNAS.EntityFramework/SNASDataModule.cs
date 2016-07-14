using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using SNAS.EntityFramework;

namespace SNAS
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(SNASCoreModule))]
    public class SNASDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
