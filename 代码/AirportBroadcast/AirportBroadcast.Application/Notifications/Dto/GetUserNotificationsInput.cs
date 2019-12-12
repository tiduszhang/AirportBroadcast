using Abp.Notifications;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}