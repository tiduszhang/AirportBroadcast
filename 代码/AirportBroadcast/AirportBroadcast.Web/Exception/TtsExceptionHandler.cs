using Abp.Authorization;
using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using Abp.UI;
using AirportBroadcast.Notifications;

namespace Ky.ScenicTicket.Web.Exception
{
    public class TtsExceptionHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        private readonly IAppNotifier _appNotifier;

        public TtsExceptionHandler(IAppNotifier appNotifier)
        {
            _appNotifier = appNotifier;
        }

        public  void HandleEvent(AbpHandledExceptionData eventData)
        {
            if(eventData.Exception is AbpAuthorizationException) return;;
            if(eventData.Exception is UserFriendlyException) return;;
            AsyncHelper.RunSync(() => _appNotifier.ExceptionAsync(eventData.Exception));

        }
    }
}