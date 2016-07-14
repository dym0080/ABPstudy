using Abp.Authorization;
using SNAS.Core.Authorization.Roles;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;

namespace SNAS.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
