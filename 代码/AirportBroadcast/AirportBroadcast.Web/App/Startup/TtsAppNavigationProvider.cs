using Abp.Application.Navigation;
using Abp.Localization;
using AirportBroadcast.Authorization;
using AirportBroadcast.Web.Navigation;

namespace AirportBroadcast.Web.App.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class TtsAppNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                    "BaseInfo",
                    L("BaseInfo"),
                     icon: "fa fa-cog",
                    requiredPermissionName: AppPermissions.Pages_BaseInfo
                    ).AddItem(new MenuItemDefinition(
                        "BaseInfoAudio",
                        L("Pages_BaseInfo_Audio"),
                       icon: "fa fa-list-ol",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_Audio
                    ).AddItem(new MenuItemDefinition(
                        "AudioLanguage",
                        L("Pages_BaseInfo_AudioLanguage"),
                       icon: "fa fa-flag-o",
                        url: "audioLanguage",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioLanguage
                    )).AddItem(new MenuItemDefinition(
                        "AudioDigit",
                        L("Pages_BaseInfo_AudioDigit"),
                        icon: "fa fa-sort-numeric-asc",
                        url: "audioDigit",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioDigit
                    )).AddItem(new MenuItemDefinition(
                        "AudioHour",
                        L("Pages_BaseInfo_AudioHour"),
                        icon: "fa fa-calendar",
                        url: "audioHour",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioHour
                    )).AddItem(new MenuItemDefinition(
                        "AudioMinite",
                        L("Pages_BaseInfo_AudioMinite"),
                        icon: "fa fa-clock-o",
                        url: "audioMinite",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioMinite
                    )).AddItem(new MenuItemDefinition(
                        "AudioConst",
                        L("Pages_BaseInfo_AudioConst"),
                        icon: "fa fa-commenting-o",
                        url: "audioConst",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioConst
                    )).AddItem(new MenuItemDefinition(
                        "AudioReason",
                        L("Pages_BaseInfo_AudioReason"),
                        icon: "fa fa-registered",
                        url: "audioReason",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioReason
                    )).AddItem(new MenuItemDefinition(
                        "AudioAirLine",
                        L("Pages_BaseInfo_AudioAirLine"),
                        icon: "fa fa-plane",
                        url: "audioAirLine",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioAirLine
                    ))
                    .AddItem(new MenuItemDefinition(
                        "AudioCity",
                        L("Pages_BaseInfo_AudioCity"),
                        icon: "fa fa-university",
                        url: "audioCity",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioCity
                    )).AddItem(new MenuItemDefinition(
                        "AudioCheckIn",
                        L("Pages_BaseInfo_AudioCheckIn"),
                        icon: "fa fa-random",
                        url: "audioCheckIn",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioCheckIn
                    )).AddItem(new MenuItemDefinition(
                        "AudioGate",
                        L("Pages_BaseInfo_AudioGate"),
                        icon: "fa fa-external-link-square",
                        url: "audioGate",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioGate
                    )).AddItem(new MenuItemDefinition(
                        "AudioTurnPlate",
                        L("Pages_BaseInfo_AudioTurnPlate"),
                        icon: "fa fa-circle-o-notch",
                        url: "audioTurnPlate",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioTurnPlate
                    )))
                    .AddItem(new MenuItemDefinition(
                        "PlaySet",
                        L("Pages_BaseInfo_PlaySet"),
                        icon: "fa fa-sliders",
                        url: "playSet",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_PlaySet
                    ))
                    .AddItem(new MenuItemDefinition(
                        "PlaySetGlobal",
                        L("Pages_BaseInfo_PlaySet_Global"),
                        icon: "fa fa-sliders",
                        url: "playSetGlobal",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_PlaySet_Global
                    ))
                   .AddItem(new MenuItemDefinition(
                        "AudioTemplte",
                        L("Pages_BaseInfo_AudioTemplte"),
                        icon: "fa fa-file-text-o",
                        url: "audioTemplte",
                        requiredPermissionName: AppPermissions.Pages_BaseInfo_AudioTemplte
                    ))
                )

               .AddItem(new MenuItemDefinition(
                        "Artificial",
                        L("Pages_Artificial"),
                        url: "artificial",
                        icon: "fa fa-exchange",
                        requiredPermissionName: AppPermissions.Pages_Artificial
                    ))
              .AddItem(new MenuItemDefinition(
                        "HandArtificial",
                        L("Pages_HandArtificial"),
                        url: "handartificial",
                        icon: "fa fa-exchange",
                        requiredPermissionName: AppPermissions.Pages_HandArtificial
                    ))
               .AddItem(new MenuItemDefinition(
                        "Logs",
                        L("Pages_Logs"),
                        icon: "icon-grid",
                        requiredPermissionName: AppPermissions.Pages_Logs
                    ).AddItem(new MenuItemDefinition(
                         "Pages_Logs_ReciveLogs",
                         L("Pages_Logs_ReciveLogs"),
                         url: "reciveLogs",
                         icon: "fa fa-list",
                         requiredPermissionName: AppPermissions.Pages_Logs_ReciveLogs
                    )).AddItem(new MenuItemDefinition(
                         "Pages_Logs_PlayLogs",
                         L("Pages_Logs_PlayLogs"),
                         url: "playLogs",
                         icon: "fa fa-list",
                         requiredPermissionName: AppPermissions.Pages_Logs_PlayLogs
                    ))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
