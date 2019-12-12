using System;
using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.MultiTenancy;

namespace AirportBroadcast.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);


        Task ExceptionAsync(Exception tenant);
    }
}
