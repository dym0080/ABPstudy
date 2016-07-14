using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using SNAS.Core.Authorization.Roles;
using SNAS.Core.Editions;
using SNAS.Core.Users;

namespace SNAS.Core.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager
            )
        {
        }
    }
}