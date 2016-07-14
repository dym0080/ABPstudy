using Abp.Application.Features;
using SNAS.Core.Authorization.Roles;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;

namespace SNAS.Core.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}