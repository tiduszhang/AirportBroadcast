using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AirportBroadcast.AudioControl;
using AirportBroadcast.AudioSets.dtos;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;
using System.Collections.Generic;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioDeviceAppService : ICrudAppService<AudioDeviceDto>
    {
        List<string> GetVoiceDevice();
    }

    [AbpAuthorize(AppPermissions.Pages_AudioSets_Device)]
    public class AudioDeviceAppService : CrudAppService<AudioDevice, AudioDeviceDto>, IAudioDeviceAppService
    {
        private readonly IWavCombine _wavCombine;
        public AudioDeviceAppService(IRepository<AudioDevice> repository, IWavCombine _wavCombine) : base(repository)
        {
            this._wavCombine = _wavCombine;
        }

        public List<string> GetVoiceDevice()
        {
            return _wavCombine.GetVoiceDevice(); 
        }
    }

}
