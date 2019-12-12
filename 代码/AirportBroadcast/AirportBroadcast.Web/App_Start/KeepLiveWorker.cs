using Abp.Dependency;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using System.Net;

namespace AirportBroadcast.Web
{
    public class KeepLiveWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {

        private readonly IWebUrlService _webUrlService;


        public KeepLiveWorker(AbpTimer timer, IWebUrlService webUrlService)
            : base(timer)
        {
            _webUrlService = webUrlService;
            Timer.Period = 60000 * 2; //5 seconds (good for tests, but normally will be more)
        }

        protected override void DoWork()
        {
            
            string url = _webUrlService.GetSiteRootAddress() + "keepalive/index";
            using (var wc = new WebClient())
            {
                try
                {
                    wc.DownloadString(url);
                }
                catch (System.Exception)
                {
                    //忽略异常
                }
            }
        }
    }
}
