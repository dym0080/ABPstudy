using Volo.Abp.Settings;

namespace Qm.Lims.Settings
{
    public class LimsSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(LimsSettings.MySetting1));
        }
    }
}
