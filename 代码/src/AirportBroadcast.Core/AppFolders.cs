using Abp.Dependency;

namespace AirportBroadcast
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; } 

        public string AudioFolder { get; set; }
    }
}