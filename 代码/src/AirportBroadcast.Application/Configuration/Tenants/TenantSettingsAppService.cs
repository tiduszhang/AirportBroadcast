﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Json;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap.Configuration;
using AirportBroadcast.AudioControl;
using AirportBroadcast.Authorization;
using AirportBroadcast.Configuration.Host.Dto;
using AirportBroadcast.Configuration.Tenants.Dto;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Security;
using AirportBroadcast.Storage;
using AirportBroadcast.Timing;
using Newtonsoft.Json;

namespace AirportBroadcast.Configuration.Tenants
{
    [AbpAuthorize]
    public class TenantSettingsAppService : AbpZeroTemplateAppServiceBase, ITenantSettingsAppService
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ITimeZoneService _timeZoneService;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IWavCombine wavCombine;
        private readonly IRepository<AudioLanguage> repository;

        public TenantSettingsAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IAbpZeroLdapModuleConfig ldapModuleConfig,
            ITimeZoneService timeZoneService,
             IWavCombine wavCombine,
             IRepository<AudioLanguage> repository,
            IBinaryObjectManager binaryObjectManager)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _ldapModuleConfig = ldapModuleConfig;
            _timeZoneService = timeZoneService;
            _binaryObjectManager = binaryObjectManager;
            this.wavCombine = wavCombine;
            this.repository = repository;
        }


        public GeneralSettingsEditDto GetPlayWay()
        {
            return new GeneralSettingsEditDto()
            {
                PlayWay = SettingManager.GetSettingValue(AppSettings.General.PlayWay)
            };
        }

        public void SetPlayWay(GeneralSettingsEditDto input)
        {
            SettingManager.ChangeSettingForApplication(AppSettings.General.PlayWay, input.PlayWay);
            if (input.PlayWay == "2")
                wavCombine.Art_Player("", "", "");

        }

        public GeneralSettingsEditDto GetPlayTimesAndCanPlayLanguages()
        {
            var result = new GeneralSettingsEditDto();
            var playTimes = SettingManager.GetSettingValue(AppSettings.General.PlayTimes);
            var timespan = SettingManager.GetSettingValue(AppSettings.General.AutoPlayGetLuaTimeSpan);
            var languages = SettingManager.GetSettingValue(AppSettings.General.CanPlayLanguages);
            var timelimit = SettingManager.GetSettingValue(AppSettings.General.UnActiveLimitTime);

            result.PlayTimes = int.Parse(playTimes);
            result.AutoPlayGetLuaTimeSpan = int.Parse(timespan);
            result.UnActiveLimitTime = int.Parse(timelimit);
            var entities = repository.GetAll().ToList();
            var CanPlayLanguages = new List<ChooseLanguages>();
            if (!string.IsNullOrEmpty(languages))
            {
                CanPlayLanguages = JsonConvert.DeserializeObject<List<ChooseLanguages>>(languages);

            }

            result.CanPlayLanguages = new List<ChooseLanguages>();

            entities.ForEach(x =>
            {
                var dto = CanPlayLanguages.FirstOrDefault(d => d.Id == x.Id);
                if (dto != null)
                {
                    result.CanPlayLanguages.Add(dto);
                }
                else
                {
                    result.CanPlayLanguages.Add(new ChooseLanguages()
                    {
                        IsChecked = true,
                        Id = x.Id,
                        LanguageName = x.Name
                    });
                }



            });





            return result;
        }

        public void SetPlayTimesAndCanPlayLanguages(GeneralSettingsEditDto input)
        {
            SettingManager.ChangeSettingForApplication(AppSettings.General.AutoPlayGetLuaTimeSpan, input.AutoPlayGetLuaTimeSpan.ToString().Trim());
            SettingManager.ChangeSettingForApplication(AppSettings.General.PlayTimes, input.PlayTimes.ToString().Trim());
            SettingManager.ChangeSettingForApplication(AppSettings.General.UnActiveLimitTime, input.UnActiveLimitTime.ToString().Trim());
            if (input.CanPlayLanguages != null)
            {
                SettingManager.ChangeSettingForApplication(AppSettings.General.CanPlayLanguages, JsonConvert.SerializeObject(input.CanPlayLanguages));
            }


        }


        

        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                UserManagement = await GetUserManagementSettingsAsync(),
                Security = await GetSecuritySettingsAsync()
            };

            if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            {
                settings.General = await GetGeneralSettingsAsync();
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                settings.Email = await GetEmailSettingsAsync();

                if (_ldapModuleConfig.IsEnabled)
                {
                    settings.Ldap = await GetLdapSettingsAsync();
                }
                else
                {
                    settings.Ldap = new LdapSettingsEditDto { IsModuleEnabled = false };
                }
            }

            return settings;
        }

        private async Task<LdapSettingsEditDto> GetLdapSettingsAsync()
        {
            return new LdapSettingsEditDto
            {
                IsModuleEnabled = true,
                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(LdapSettingNames.IsEnabled),
                Domain = await SettingManager.GetSettingValueAsync(LdapSettingNames.Domain),
                UserName = await SettingManager.GetSettingValueAsync(LdapSettingNames.UserName),
                Password = await SettingManager.GetSettingValueAsync(LdapSettingNames.Password),
            };
        }

        private async Task<EmailSettingsEditDto> GetEmailSettingsAsync()
        {
            return new EmailSettingsEditDto
            {
                DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password),
                SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
            };
        }

        private async Task<GeneralSettingsEditDto> GetGeneralSettingsAsync()
        {
            var settings = new GeneralSettingsEditDto();

            if (Clock.SupportsMultipleTimezone)
            {
                var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                settings.Timezone = timezone;
                settings.TimezoneForComparison = timezone;
            }

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);

            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<TenantUserManagementSettingsEditDto> GetUserManagementSettingsAsync()
        {
            return new TenantUserManagementSettingsEditDto
            {
                AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowSelfRegistration),
                IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault),
                IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration)
            };
        }

        private async Task<SecuritySettingsEditDto> GetSecuritySettingsAsync()
        {
            var passwordComplexitySetting = await SettingManager.GetSettingValueAsync(AppSettings.Security.PasswordComplexity);
            var defaultPasswordComplexitySetting = await SettingManager.GetSettingValueForApplicationAsync(AppSettings.Security.PasswordComplexity);

            var settings = new SecuritySettingsEditDto
            {
                UseDefaultPasswordComplexitySettings = passwordComplexitySetting == defaultPasswordComplexitySetting,
                PasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(passwordComplexitySetting),
                DefaultPasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(defaultPasswordComplexitySetting),
                UserLockOut = await GetUserLockOutSettingsAsync()
            };

            settings.TwoFactorLogin = await GetTwoFactorLoginSettingsAsync();

            return settings;
        }

        private async Task<UserLockOutSettingsEditDto> GetUserLockOutSettingsAsync()
        {
            return new UserLockOutSettingsEditDto
            {
                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled),
                MaxFailedAccessAttemptsBeforeLockout = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout),
                DefaultAccountLockoutSeconds = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds)
            };
        }

        private Task<bool> IsTwoFactorLoginEnabledForApplicationAsync()
        {
            return SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled);
        }

        private async Task<TwoFactorLoginSettingsEditDto> GetTwoFactorLoginSettingsAsync()
        {
            var settings = new TwoFactorLoginSettingsEditDto
            {
                IsEnabledForApplication = await IsTwoFactorLoginEnabledForApplicationAsync()
            };

            if (_multiTenancyConfig.IsEnabled && !settings.IsEnabledForApplication)
            {
                return settings;
            }

            settings.IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled);
            settings.IsRememberBrowserEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled);

            if (!_multiTenancyConfig.IsEnabled)
            {
                settings.IsEmailProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled);
                settings.IsSmsProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled);
            }

            return settings;
        }

       

        #region Update Settings

        public async Task UpdateAllSettings(TenantSettingsEditDto input)
        {
            await UpdateUserManagementSettingsAsync(input.UserManagement);
            await UpdateSecuritySettingsAsync(input.Security);

            //Time Zone
            if (Clock.SupportsMultipleTimezone)
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                input.ValidateHostSettings();

                await UpdateEmailSettingsAsync(input.Email);

                if (_ldapModuleConfig.IsEnabled)
                {
                    await UpdateLdapSettingsAsync(input.Ldap);
                }
            }



        }

        private async Task UpdateLdapSettingsAsync(LdapSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.IsEnabled, input.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Domain, input.Domain.IsNullOrWhiteSpace() ? null : input.Domain);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.UserName, input.UserName.IsNullOrWhiteSpace() ? null : input.UserName);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Password, input.Password.IsNullOrWhiteSpace() ? null : input.Password);
        }

        private async Task UpdateEmailSettingsAsync(EmailSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, input.SmtpPort.ToString(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
        }

        private async Task UpdateUserManagementSettingsAsync(TenantUserManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.AllowSelfRegistration,
                settings.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault,
                settings.IsNewRegisteredUserActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                settings.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.UseCaptchaOnRegistration,
                settings.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
        }

        private async Task UpdateSecuritySettingsAsync(SecuritySettingsEditDto settings)
        {
            if (settings.UseDefaultPasswordComplexitySettings)
            {
                await SettingManager.ChangeSettingForTenantAsync(
                    AbpSession.GetTenantId(),
                    AppSettings.Security.PasswordComplexity,
                    settings.DefaultPasswordComplexity.ToJsonString()
                );
            }
            else
            {
                await SettingManager.ChangeSettingForTenantAsync(
                    AbpSession.GetTenantId(),
                    AppSettings.Security.PasswordComplexity,
                    settings.PasswordComplexity.ToJsonString()
                );
            }

            await UpdateUserLockOutSettingsAsync(settings.UserLockOut);
            await UpdateTwoFactorLoginSettingsAsync(settings.TwoFactorLogin);
        }

        private async Task UpdateUserLockOutSettingsAsync(UserLockOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled, settings.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, settings.DefaultAccountLockoutSeconds.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, settings.MaxFailedAccessAttemptsBeforeLockout.ToString());
        }

        private async Task UpdateTwoFactorLoginSettingsAsync(TwoFactorLoginSettingsEditDto settings)
        {
            if (_multiTenancyConfig.IsEnabled &&
                !await IsTwoFactorLoginEnabledForApplicationAsync()) //Two factor login can not be used by tenants if disabled by the host
            {
                return;
            }

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled, settings.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, settings.IsRememberBrowserEnabled.ToString(CultureInfo.InvariantCulture).ToLower());

            if (!_multiTenancyConfig.IsEnabled)
            {
                //These settings can only be changed by host, in a multitenant application.
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled, settings.IsEmailProviderEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled, settings.IsSmsProviderEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            }
        }

        #endregion

        #region Others

        public async Task ClearLogo()
        {
            var tenant = await GetCurrentTenantAsync();

            if (!tenant.HasLogo())
            {
                return;
            }

            var logoObject = await _binaryObjectManager.GetOrNullAsync(tenant.LogoId.Value);
            if (logoObject != null)
            {
                await _binaryObjectManager.DeleteAsync(tenant.LogoId.Value);
            }

            tenant.ClearLogo();
        }

        public async Task ClearCustomCss()
        {
            var tenant = await GetCurrentTenantAsync();

            if (!tenant.CustomCssId.HasValue)
            {
                return;
            }

            var cssObject = await _binaryObjectManager.GetOrNullAsync(tenant.CustomCssId.Value);
            if (cssObject != null)
            {
                await _binaryObjectManager.DeleteAsync(tenant.CustomCssId.Value);
            }

            tenant.CustomCssId = null;
        }


        #endregion


    }
}