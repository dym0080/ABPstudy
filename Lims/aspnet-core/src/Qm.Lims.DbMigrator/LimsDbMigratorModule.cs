using Qm.Lims.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Qm.Lims.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LimsEntityFrameworkCoreModule),
        typeof(LimsApplicationContractsModule)
        )]
    public class LimsDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
