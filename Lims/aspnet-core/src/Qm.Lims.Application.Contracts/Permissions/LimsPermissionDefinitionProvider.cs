using Qm.Lims.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Qm.Lims.Permissions
{
    public class LimsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(LimsPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(LimsPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LimsResource>(name);
        }
    }
}
