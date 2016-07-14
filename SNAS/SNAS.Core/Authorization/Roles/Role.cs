using Abp.Authorization.Roles;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;

namespace SNAS.Core.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {
 
    }
}