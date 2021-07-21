using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DynamicField.Configuration.Dto;

namespace DynamicField.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DynamicFieldAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
