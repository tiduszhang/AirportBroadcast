using Abp.Authorization;
using AirportBroadcast.Authorization.Roles;
using AirportBroadcast.Authorization.Users;

namespace AirportBroadcast.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
