using AirportBroadcast.Dto;

namespace AirportBroadcast.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}