using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.MultiTenancy;

namespace AirportBroadcast.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
