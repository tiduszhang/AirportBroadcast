using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioLanguageAppService : ICrudAppService<AudioLanguageDto>
    {

    }

    [AbpAuthorize(AppPermissions.Pages_BaseInfo_AudioLanguage)]
    public class AudioLanguageAppService : CrudAppService<AudioLanguage, AudioLanguageDto>, IAudioLanguageAppService
    {
        public AudioLanguageAppService(IRepository<AudioLanguage> repository) : base(repository)
        { 

        }
    }

}
