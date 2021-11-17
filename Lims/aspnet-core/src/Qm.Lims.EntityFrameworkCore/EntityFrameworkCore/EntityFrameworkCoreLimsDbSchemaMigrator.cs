using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qm.Lims.Data;
using Volo.Abp.DependencyInjection;

namespace Qm.Lims.EntityFrameworkCore
{
    public class EntityFrameworkCoreLimsDbSchemaMigrator
        : ILimsDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreLimsDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the LimsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<LimsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
