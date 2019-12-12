using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioConstAppService : ICrudAppService<AudioConstDto,int, GetAllAudioInputDto>
    {
    }

    [AbpAuthorize(AppPermissions.Pages_BaseInfo_AudioConst)]
    public class AudioConstAppService : CrudAppService<AudioConst, AudioConstDto,int, GetAllAudioInputDto>, IAudioConstAppService
    {
        public AudioConstAppService(IRepository<AudioConst> repository) : base(repository)
        {
        }

        protected override IQueryable<AudioConst> CreateFilteredQuery(GetAllAudioInputDto input)
        {
            var query = base.CreateFilteredQuery(input);
            if (input.LanguageId.HasValue)
                query = query.Where(x => x.LanguageId == input.LanguageId.Value);

            if (!string.IsNullOrEmpty(input.Code))
                query = query.Where(x => x.ConstNo.Contains(input.Code));

            if (!string.IsNullOrEmpty(input.FileName))
                query = query.Where(x => x.FileName.Contains(input.FileName));

            return query;


        }
    }
}
