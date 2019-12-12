using Abp.Application.Services;
using AirportBroadcast.AudioControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.PlayAudio
{
    public interface IWavCombineAppService : IApplicationService
    {
        void TestPlay(string pathName, int playNum, string ports);
    }

    public class WavCombineAppService : AbpZeroTemplateAppServiceBase, IWavCombineAppService
    {
        private readonly IWavCombine combine;


        public WavCombineAppService(IWavCombine combine)
        {
            this.combine = combine;

        }

        public void TestPlay(string pathName,int playNum,string ports) {
            //combine.Player(pathName, playNum, ports);
        }

    }
}
