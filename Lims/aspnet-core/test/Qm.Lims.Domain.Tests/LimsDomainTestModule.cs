using Qm.Lims.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Qm.Lims
{
    [DependsOn(
        typeof(LimsEntityFrameworkCoreTestModule)
        )]
    public class LimsDomainTestModule : AbpModule
    {

    }
}