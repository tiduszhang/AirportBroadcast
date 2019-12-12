using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AirportBroadcast.Common.Dto;

namespace AirportBroadcast.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<ComboboxItemDto>> GetEditionsForCombobox();

        Task<ListResultDto<LanguageForChoose>> GetAudioLanguageForCombobox();



        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        string GetDefaultEditionName();

        bool CheckFileIsExist(FilePathAndNameInputDto input);



    }
}