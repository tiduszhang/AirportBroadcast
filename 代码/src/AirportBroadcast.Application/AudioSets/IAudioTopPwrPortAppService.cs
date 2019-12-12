using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AirportBroadcast.AudioSets.dtos;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioTopPwrPortAppService : ICrudAppService<TopPwrPortDto>
    {

    }

    [AbpAuthorize(AppPermissions.Pages_AudioSets_TopPwrPort)]
    public class AudioTopPwrPortAppService : CrudAppService<TopPwrPort, TopPwrPortDto>, IAudioTopPwrPortAppService
    {
        public AudioTopPwrPortAppService(IRepository<TopPwrPort> repository) : base(repository)
        { 

        }
    }

}
