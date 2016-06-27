using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using MyEventCloud.EntityFramework;

namespace MyEventCloud.Migrator
{
    [DependsOn(typeof(MyEventCloudDataModule))]
    public class MyEventCloudMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<MyEventCloudDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}