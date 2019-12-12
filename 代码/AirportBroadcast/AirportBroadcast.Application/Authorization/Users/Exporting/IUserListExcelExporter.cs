using System.Collections.Generic;
using AirportBroadcast.Authorization.Users.Dto;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}