using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Uow;
using Microsoft.Owin.Security;
using AirportBroadcast.Authorization.Roles;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.MultiTenancy;

namespace AirportBroadcast.Web.Auth
{
    public class SignInManager : AbpSignInManager<Tenant, Role, User>
    {
        public SignInManager(
            UserManager userManager, 
            IAuthenticationManager authenticationManager, 
            ISettingManager settingManager,
            IUnitOfWorkManager unitOfWorkManager) 
            : base(
                  userManager, 
                  authenticationManager,
                  settingManager,
                  unitOfWorkManager)
        {
        }
    }
}