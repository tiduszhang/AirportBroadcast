using System;
using Abp.Application.Services.Dto;

namespace AirportBroadcast.Authorization.Users
{
    public class LinkedUserDto : EntityDto<long>
    {
        public int? TenantId { get; set; }

        public string TenancyName { get; set; }

        public string Username { get; set; }

        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否可设置所有乘车站
        /// </summary>
        public bool IsCanSetAllCarryStation { get; set; }

        public object GetShownLoginName(bool multiTenancyEnabled)
        {
            if (!multiTenancyEnabled)
            {
                return Username;
            }

            return string.IsNullOrEmpty(TenancyName)
                ? ".\\" + Username
                : TenancyName + "\\" + Username;
        }
    }
}