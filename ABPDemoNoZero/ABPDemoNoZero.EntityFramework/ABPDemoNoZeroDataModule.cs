using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using ABPDemoNoZero.EntityFramework;

namespace ABPDemoNoZero
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(ABPDemoNoZeroCoreModule))]
    public class ABPDemoNoZeroDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<ABPDemoNoZeroDbContext>(null);
        }
    }
}
