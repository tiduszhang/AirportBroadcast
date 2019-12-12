using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.PlayAudio
{

    [DependsOn(typeof(AbpZeroTemplateCoreModule))]
    public class AudioControlModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.Auditing.SaveReturnValues = true;
            //Declare entity types
      
            //Add/remove localization sources
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "AbpZeroTemplate",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "AirportBroadcast.Localization.AbpZeroTemplate"
                        )
                    )
                );
 
 
            Configuration.UnitOfWork.IsTransactional = false;
             
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            

        }
    }
}
