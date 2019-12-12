using Abp.Configuration;
using Abp.Dependency;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Castle.Core.Logging;
using AirportBroadcast.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportBroadcast.Tasks
{
    public class PushDataWorkerManager : RunnableBase, IPushDataWorkerManager
    {
        private readonly Dictionary<int, PushFetchDataWorker> _pushFetchDataWorker;
        private readonly Dictionary<int, PushOrderDataWorker> _pushOrderDataWorker;

        public ISettingManager SettingManager { get; set; }

        private readonly ILogger _logger;

        private readonly IIocResolver _iocResolver;
        public PushDataWorkerManager(ILogger logger, IIocResolver iocResolver)
        {
            _pushFetchDataWorker = new Dictionary<int, PushFetchDataWorker>();
            _pushOrderDataWorker = new Dictionary<int, PushOrderDataWorker>();
            _logger = logger;
            _iocResolver = iocResolver;
        }

        public void Add(IBackgroundWorker worker)
        {
            throw new NotImplementedException();
        }

        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            _pushFetchDataWorker.Values.ToList().ForEach(_iocResolver.Release);
            _pushFetchDataWorker.Clear();

            _pushOrderDataWorker.Values.ToList().ForEach(_iocResolver.Release);
            _pushOrderDataWorker.Clear();
        }

        public void PushFetchDataData(int equipmentId)
        {
            //开启取票推送   
            try
            {
                PushFetchDataWorker worker = null;
                if (!_pushFetchDataWorker.ContainsKey(equipmentId))
                {
                    worker = IocManager.Instance.Resolve<PushFetchDataWorker>();
                    worker.EquipmentId = equipmentId;
                    _pushFetchDataWorker.Add(equipmentId, worker);
                    worker.SetTimerPeriod(int.TryParse(SettingManager.GetSettingValue(AppSettings.PushToOnline.TimerPeriod).ToString(), out int result) ? result : 0);
                    worker.Start();
                }
                else
                {
                    worker = _pushFetchDataWorker[equipmentId];
                    if (worker.IsRunning)
                    {
                        worker.IsRunNow = true;
                    }
                    else
                    {
                        worker.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("取票数据推送异常！", ex);
            }
        }

        public void PushOrderData(int equipmentId)
        {
            try
            {
                PushOrderDataWorker worker = null;
                if (!_pushOrderDataWorker.ContainsKey(equipmentId))
                {
                    worker = IocManager.Instance.Resolve<PushOrderDataWorker>();
                    worker.EquipmentId = equipmentId;
                    _pushOrderDataWorker.Add(equipmentId, worker);
                    worker.SetTimerPeriod(int.TryParse(SettingManager.GetSettingValue(AppSettings.PushToOnline.TimerPeriod).ToString(), out int result) ? result : 0);
                    worker.Start();
                }
                else
                {
                    worker = _pushOrderDataWorker[equipmentId];
                    if (worker.IsRunning)
                    {
                        worker.IsRunNow = true;
                    }
                    else
                    {
                        worker.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("订单数据推送异常！", ex);
            }
        }

        public override void WaitToStop()
        {
            _pushFetchDataWorker.Values.ToList().ForEach(job => job.WaitToStop());
            _pushOrderDataWorker.Values.ToList().ForEach(job => job.WaitToStop());

            base.WaitToStop();
        }

        public override void Stop()
        {
            _pushFetchDataWorker.Values.ToList().ForEach(job => job.Stop());
            _pushOrderDataWorker.Values.ToList().ForEach(job => job.Stop());
            base.Stop();
        }
    }
}
