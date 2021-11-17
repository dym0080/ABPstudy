using Volo.Abp.Modularity;

namespace Qm.Lims
{
    [DependsOn(
        typeof(LimsApplicationModule),
        typeof(LimsDomainTestModule)
        )]
    public class LimsApplicationTestModule : AbpModule
    {

    }
}