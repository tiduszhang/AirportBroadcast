using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Castle.Components.DictionaryAdapter;
using AirportBroadcast.Friendships.Dto;

namespace AirportBroadcast.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }

        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}