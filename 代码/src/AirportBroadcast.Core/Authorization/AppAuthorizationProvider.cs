using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace AirportBroadcast.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            //var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
          
            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);



            // administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_AudioSets_Device, L("Pages_AudioSets_Device"));
            administration.CreateChildPermission(AppPermissions.Pages_AudioSets_TopPwrPort, L("Pages_AudioSets_TopPwrPort"));

            var baseInfo = pages.CreateChildPermission(AppPermissions.Pages_BaseInfo, L("BaseInfo"));
            var audio = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_Audio, L("Pages_BaseInfo_Audio"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioLanguage, L("Pages_BaseInfo_AudioLanguage"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioAirLine, L("Pages_BaseInfo_AudioAirLine"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioCheckIn, L("Pages_BaseInfo_AudioCheckIn"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioCity, L("Pages_BaseInfo_AudioCity"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioConst, L("Pages_BaseInfo_AudioConst"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioDigit, L("Pages_BaseInfo_AudioDigit"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioGate, L("Pages_BaseInfo_AudioGate"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioHour, L("Pages_BaseInfo_AudioHour"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioMinite, L("Pages_BaseInfo_AudioMinite"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioReason, L("Pages_BaseInfo_AudioReason"));
            audio.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioTurnPlate, L("Pages_BaseInfo_AudioTurnPlate"));

            baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_AudioTemplte, L("Pages_BaseInfo_AudioTemplte"));
            baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaySet, L("Pages_BaseInfo_PlaySet"));
            baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaySet_Global, L("Pages_BaseInfo_PlaySet_Global"));

            var sales = pages.CreateChildPermission(AppPermissions.Pages_Logs, L("Pages_Logs"));
            sales.CreateChildPermission(AppPermissions.Pages_Logs_PlayLogs, L("Pages_Logs_PlayLogs"));
            sales.CreateChildPermission(AppPermissions.Pages_Logs_ReciveLogs, L("Pages_Logs_ReciveLogs"));

            var artificial = pages.CreateChildPermission(AppPermissions.Pages_Artificial, L("Pages_Artificial"));
            var handArtificial = pages.CreateChildPermission(AppPermissions.Pages_HandArtificial, L("Pages_HandArtificial"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
