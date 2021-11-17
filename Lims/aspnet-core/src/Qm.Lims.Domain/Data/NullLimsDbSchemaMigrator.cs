using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Qm.Lims.Data
{
    /* This is used if database provider does't define
     * ILimsDbSchemaMigrator implementation.
     */
    public class NullLimsDbSchemaMigrator : ILimsDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}