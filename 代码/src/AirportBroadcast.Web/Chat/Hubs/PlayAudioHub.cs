using Abp.Dependency;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using AirportBroadcast.Chat;
using AirportBroadcast.Chat.Dto;
using AirportBroadcast.Sessions;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirportBroadcast.Web.Chat.Hubs
{
    public class PlayAudioHub : Hub, IPlayAudioHub, ISingletonDependency
    {
        private readonly IHubContext _hubContext;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        public IAbpSession AbpSession { get; set; }

      
        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="i">J 进港，C 出港</param>
        public void RefreshData(string deporArrCode)
        {
            try
            {
                //AbpSession.GetUserId();
                if (Context != null) {
                    Clients.All.RefreshData(deporArrCode);
                }           
            }catch(Exception ex)
            {

            } 
        }

        public void RefreshNowPlayAudio(TpPortDto port)
        {
            try
            {
             //   AbpSession.GetUserId();
                if (Context != null)
                {
                    Clients.All.RefreshNowPlayAudio(port);
                }
            }
            catch (Exception ex)
            {

            }
 
        }

        //客户端连接上时，会进入到此方法中
        public override Task OnConnected()
        {
            return base.OnConnected();
        }


        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}