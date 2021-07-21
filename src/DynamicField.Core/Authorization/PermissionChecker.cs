using Abp.Authorization;
using DynamicField.Authorization.Roles;
using DynamicField.Authorization.Users;

namespace DynamicField.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
