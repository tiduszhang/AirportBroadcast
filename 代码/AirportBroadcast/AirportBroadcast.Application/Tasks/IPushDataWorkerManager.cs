using Abp.Dependency;
using Abp.Threading.BackgroundWorkers;
using System;

namespace AirportBroadcast.Tasks
{
    public interface IPushDataWorkerManager : IBackgroundWorkerManager, ISingletonDependency, IDisposable
    {
        void Dispose();
        void PushFetchDataData(int equipmentId);
      
        void PushOrderData(int equipmentId);
        void Stop();
        void WaitToStop();
    }
}
