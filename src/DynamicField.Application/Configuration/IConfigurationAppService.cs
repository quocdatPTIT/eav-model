using System.Threading.Tasks;
using DynamicField.Configuration.Dto;

namespace DynamicField.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
