using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Json;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using System.Web.Http.Cors; 

namespace AirportBroadcast.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(AbpZeroTemplateApplicationModule))]
    public class AbpZeroTemplateWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(AbpZeroTemplateApplicationModule).Assembly, "app")
                .Build();

             
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            var cors = new EnableCorsAttribute("*", "*", "*");
            
            GlobalConfiguration.Configuration.EnableCors(cors);
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
            ConfigureSwaggerUi(); //Remove this line to disable swagger UI.
                                  ///swagger/ui/index
                                  ///
            // Configuration.Modules.AbpConfiguration.Validation.
                        
        }


        public override void PostInitialize()
        {
            base.PostInitialize();
            var converters =
             Configuration.Modules.AbpWebApi()
                 .HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters;
            //所有的枚举返回的都是字符串
            converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
          

            //foreach (var jsonConverter in converters)
            //{
            //    if (jsonConverter is AbpDateTimeConverter)
            //    {
            //        var tempjsonConverter = jsonConverter as AbpDateTimeConverter;
            //        tempjsonConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //    }
            //}

        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    //var commentsFileName = "bin/AirportBroadcast.Application.XML";
                    //var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.SingleApiVersion("v1", "Api接口文档-可在线调试");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    //c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(AbpZeroTemplateWebApiModule)), "AirportBroadcast.WebApi.Scripts.Swagger-Custom.js");
                });
        }
    }
}
