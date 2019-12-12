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
using System.Linq;


namespace AirportBroadcast.Application
{
    /// <summary>
    /// 自动播放行李提取
    /// </summary>
    public class AutoPlayGetLuaTask : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<AirshowData, long> repository; 
        private readonly IRepository<ReceiveJson, long> _rcrepository;
        private readonly IRepository<PlayAudioLog, long> _logRepository;
        private readonly ICommAudioTempleAppService _play;

        public AutoPlayGetLuaTask(AbpTimer timer,
            IRepository<AirshowData, long> repository,
            IRepository<ReceiveJson, long> _rcrepository,
              ICommAudioTempleAppService _play,
            IRepository<PlayAudioLog, long> _logRepository
            ) : base(timer)
        {
            // Timer.Period = 1000 * 60;
            Timer.Period = 1000 * 30; //30秒 (good for tests, but normally will be more)
            this.repository = repository;
            this._rcrepository = _rcrepository;
            this._logRepository = _logRepository;
            this._play = _play;
        }
         

        [UnitOfWork(false)]
        protected override void DoWork()
        {
           var timeStr = SettingManager.GetSettingValue(AppSettings.General.AutoPlayGetLuaTimeSpan);
            var lastTime = DateTime.Now.AddMinutes(-Double.Parse(timeStr));
           var items = repository.GetAll()
                .Where(x => x.DeporArrCode == "J")
                .Where(x => x.FlightCirculationStatus == "ARR" || x.FlightCirculationStatus == "NST")
                .Where(x => x.ReciveTime < lastTime).ToList();

            if(items!=null && items.Count() > 0)
            {
                items.ForEach(entity => {
                    entity.FlightCirculationStatus = "FBAG";
                    repository.Update(entity);
                    _logRepository.Insert(new PlayAudioLog() {
                        FileName = "---",
                        Remark = string.Format("航班{0}到达之后，隔{1}分钟之后，自动播放行李提取",entity.FlightNo2,timeStr)}
                    );
                    var dto = Mapper.Map<AirShowDataDto>(entity);
                      _play.PlayAudioTemple(dto, false);

                });

               
            }
        }

    }
}
