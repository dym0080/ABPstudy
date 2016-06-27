using Abp.Authorization;
using MyEventCloud.Authorization.Roles;
using MyEventCloud.MultiTenancy;
using MyEventCloud.Users;

namespace MyEventCloud.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
