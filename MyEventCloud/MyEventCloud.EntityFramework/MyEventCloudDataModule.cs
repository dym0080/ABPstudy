using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using MyEventCloud.EntityFramework;

namespace MyEventCloud
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(MyEventCloudCoreModule))]
    public class MyEventCloudDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MyEventCloudDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
