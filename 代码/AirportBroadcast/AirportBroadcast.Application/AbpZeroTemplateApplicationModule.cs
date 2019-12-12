using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using AirportBroadcast.ActiveMQ;
using AirportBroadcast.Application;
using AirportBroadcast.Authorization; 

namespace AirportBroadcast
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(AbpZeroTemplateCoreModule))]
    public class AbpZeroTemplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper mappings
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });             
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            var cancelworker = IocManager.Resolve<AutoPlayGetLuaTask>();
            var remo = IocManager.Resolve<RemoveLogsTask>();
            
            workManager.Add(cancelworker);
            workManager.Add(remo);

            var activeMQListener = IocManager.Resolve<ActiveMQListener>();
            activeMQListener.Initialize(0);//
            Console.WriteLine("modal activeMQListener UUID" + activeMQListener.UUID);
        }
    }
}
