using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;
using Abp.Json;
using Abp.Zero.Configuration;
using AirportBroadcast.Security;

namespace AirportBroadcast.Configuration
{
    /// <summary>
    /// Defines settings for the application.
    /// See <see cref="AppSettings"/> for setting names.
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            context.Manager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled).DefaultValue = false.ToString().ToLowerInvariant();

            var defaultPasswordComplexitySetting = new PasswordComplexitySetting
            {
                MinLength = 6,
                MaxLength = 10,
                UseNumbers = true,
                UseUpperCaseLetters = false,
                UseLowerCaseLetters = true,
                UsePunctuations = false,
            };

            return new[]
                   {
                       //Host settings
                        new SettingDefinition(AppSettings.TenantManagement.AllowSelfRegistration,ConfigurationManager.AppSettings[AppSettings.TenantManagement.UseCaptchaOnRegistration] ?? "true"),
                        new SettingDefinition(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault,ConfigurationManager.AppSettings[AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault] ??"false"),
                        new SettingDefinition(AppSettings.TenantManagement.UseCaptchaOnRegistration,ConfigurationManager.AppSettings[AppSettings.TenantManagement.UseCaptchaOnRegistration] ?? "true"),
                        new SettingDefinition(AppSettings.TenantManagement.DefaultEdition,ConfigurationManager.AppSettings[AppSettings.TenantManagement.DefaultEdition] ?? ""),
                        new SettingDefinition(AppSettings.Security.PasswordComplexity, defaultPasswordComplexitySetting.ToJsonString(),scopes: SettingScopes.Application | SettingScopes.Tenant),

                        //Tenant settings
                        new SettingDefinition(AppSettings.UserManagement.AllowSelfRegistration, ConfigurationManager.AppSettings[AppSettings.UserManagement.AllowSelfRegistration] ?? "true", scopes: SettingScopes.Tenant),
                        new SettingDefinition(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, ConfigurationManager.AppSettings[AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault] ?? "false", scopes: SettingScopes.Tenant),
                        new SettingDefinition(AppSettings.UserManagement.UseCaptchaOnRegistration, ConfigurationManager.AppSettings[AppSettings.UserManagement.UseCaptchaOnRegistration] ?? "true", scopes: SettingScopes.Tenant),

                        new SettingDefinition(AppSettings.General.PlayWay,"1"),//1自动播放，2手动播放
                        new SettingDefinition(AppSettings.General.AutoPlayGetLuaTimeSpan,"20"),//自动播放行李提取时间间隔  单位分钟
                        new SettingDefinition(AppSettings.General.PlayTimes,"1"),
                        new SettingDefinition(AppSettings.General.CanPlayLanguages,""),
                        new SettingDefinition(AppSettings.General.UnActiveLimitTime,"15"),
            };
        }
    }
}
