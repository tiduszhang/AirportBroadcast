using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.IO;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using AirportBroadcast.Web.App.Startup;//SPA!
using AirportBroadcast.Web.Areas.Mpa.Startup;//MPA!
using AirportBroadcast.Web.Bundling;
using AirportBroadcast.Web.Navigation;
using AirportBroadcast.Web.Routing;
using AirportBroadcast.WebApi;
using Abp.Threading.BackgroundWorkers;
using System;
using Abp.Dependency;
using AirportBroadcast.PlayAudio;
using AirportBroadcast.AudioControl;

namespace AirportBroadcast.Web
{
    /// <summary>
    /// Web module of the application.
    /// This is the most top and entrance module that depends on others.
    /// </summary>
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(AbpZeroOwinModule),
        typeof(AbpZeroTemplateDataModule),
        typeof(AbpZeroTemplateApplicationModule),
        typeof(AbpZeroTemplateWebApiModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpRedisCacheModule), //AbpRedisCacheModule dependency can be removed if not using Redis cache
        typeof(AbpHangfireModule))] //AbpHangfireModule dependency can be removed if not using Hangfire
    public class AbpZeroTemplateWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<AppNavigationProvider>();//SPA!
            Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
            Configuration.Navigation.Providers.Add<TtsAppNavigationProvider>();//MPA!

            Configuration.Navigation.Providers.Add<MpaNavigationProvider>();//MPA!

            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = WebUrlService.WebSiteRootAddress;
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;


            IocManager.Register<IWavCombine, WavCombine>(DependencyLifeStyle.Singleton);
            //Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

            //Uncomment these lines to use HangFire as background job manager.
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});

            //Uncomment this line to use Redis cache instead of in-memory cache.
            // Configuration.Caching.UseRedis();

            //配置使用Redis缓存
            //Configuration.Caching.UseRedis();

            ////配置所有Cache的默认过期时间为30分钟
            //Configuration.Caching.ConfigureAll(cache =>
            //{
            //    cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30);
            //});



        }

        public override void Initialize()
        {
            //Dependency Injection
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
                );
          
            //Areas
            AreaRegistration.RegisterAllAreas();

            //Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Bundling
            BundleTable.Bundles.IgnoreList.Clear();
            CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
            AppBundleConfig.RegisterBundles(BundleTable.Bundles);//SPA!
            FrontEndBundleConfig.RegisterBundles(BundleTable.Bundles);
            MpaBundleConfig.RegisterBundles(BundleTable.Bundles);//MPA!
        }

        public override void PostInitialize()
        {
            var server = HttpContext.Current.Server;
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.SampleProfileImagesFolder = server.MapPath("~/Common/Images/SampleProfilePics");
            appFolders.TempFileDownloadFolder = server.MapPath("~/Temp/Downloads");
            appFolders.WebLogsFolder = server.MapPath("~/App_Data/Logs");
            appFolders.AudioFolder = server.MapPath("~/Content/Audios"); 

            try {
                DirectoryHelper.CreateIfNotExists(appFolders.TempFileDownloadFolder);
                DirectoryHelper.CreateIfNotExists(appFolders.AudioFolder);
            } catch { }


            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<KeepLiveWorker>());

            
        }
    }
}
