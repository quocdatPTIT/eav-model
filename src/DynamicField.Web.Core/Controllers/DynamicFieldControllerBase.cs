using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DynamicField.Controllers
{
    public abstract class DynamicFieldControllerBase: AbpController
    {
        protected DynamicFieldControllerBase()
        {
            LocalizationSourceName = DynamicFieldConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
