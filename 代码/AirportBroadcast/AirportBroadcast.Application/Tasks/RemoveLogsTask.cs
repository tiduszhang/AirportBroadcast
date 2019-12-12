using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using AirportBroadcast.ActiveMQ;
using AirportBroadcast.ActiveMQ.Dto;
using AirportBroadcast.Configuration;
using AirportBroadcast.Domain.activeMq;
using AirportBroadcast.Domain.baseinfo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace AirportBroadcast.Application
{
    /// <summary>
    /// 自动播放行李提取
    /// </summary>
    public class RemoveLogsTask : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<AirshowData, long> repository; 
        private readonly IRepository<ReceiveJson, long> _rcrepository;
        private readonly IRepository<PlayAudioLog, long> _logRepository;
        private readonly ICommAudioTempleAppService _play;
        private readonly IAppFolders appFolders;

        public RemoveLogsTask(AbpTimer timer,
            IRepository<AirshowData, long> repository,
            IRepository<ReceiveJson, long> _rcrepository,
              ICommAudioTempleAppService _play,
               IAppFolders appFolders,
            IRepository<PlayAudioLog, long> _logRepository
            ) : base(timer)
        {
            // Timer.Period = 1000 * 60;
            Timer.Period =   1000 * 300; //30秒 (good for tests, but normally will be more)
            this.repository = repository;
            this._rcrepository = _rcrepository;
            this._logRepository = _logRepository;
            this._play = _play;
            this.appFolders = appFolders;
        }
         

        [UnitOfWork(false)]
        protected override void DoWork()
        {
            var todayPath = DateTime.Today.ToString("yyyy-MM-dd");
            var yestodayPath = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            var lastTime = DateTime.Today.AddDays(-2);

          //  var items = repository.GetAll().Where(x => x.ReciveTime < lastTime);
           if(appFolders==null || appFolders.AudioFolder == null)
            {
                return;
            }
            string root_path = Path.Combine(appFolders.AudioFolder, "Temp");
            var dirs = Directory.EnumerateDirectories(root_path);
            foreach(var dir in dirs)
            {
                if(dir.EndsWith(todayPath) || dir.EndsWith(yestodayPath))
                {

                }
                else
                {
                    var pat = Path.Combine(root_path, dir);
                    Directory.Delete(pat,true);
                }

            }

        }

    }
}
