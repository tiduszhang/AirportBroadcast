namespace AirportBroadcast.Configuration
{
    /// <summary>
    /// Defines string constants for setting names in the application.
    /// See <see cref="AppSettingProvider"/> for setting definitions.
    /// </summary>
    public static class AppSettings
    {
        public static class General
        {
            //no setting yet
            public const string PlayWay = "App.General.PlayWay";//1自动播放，2手动播放
            
            public const string AutoPlayGetLuaTimeSpan = "App.General.AutoPlayGetLuaTimeSpan";//自动播放行李提取时间间隔

            public const string PlayTimes = "App.General.PlayTimes";//所有语句播放次数

            public const string CanPlayLanguages = "App.General.CanPlayLanguages"; //所有设置中，可播放的语种

            public const string UnActiveLimitTime = "App.General.UnActiveLimitTime"; //时限
        }

        public static class TenantManagement
        {
            public const string AllowSelfRegistration = "App.TenantManagement.AllowSelfRegistration";
            public const string IsNewRegisteredTenantActiveByDefault = "App.TenantManagement.IsNewRegisteredTenantActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.TenantManagement.UseCaptchaOnRegistration";
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition"; 
        }

        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";
            public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
        }

        public static class Security
        {
            public const string PasswordComplexity = "App.Security.PasswordComplexity";
        }
         
       
    }
}