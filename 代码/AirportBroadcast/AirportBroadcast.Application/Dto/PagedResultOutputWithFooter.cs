using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Dto
{
    /// <summary>
    /// 扩展 Abp分页类，添加页脚信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class PagedResultOutputWithFooter<T, K> : PagedResultDto<T>
    {
        public PagedResultOutputWithFooter(int totalCount, IReadOnlyList<T> items, K footer)
            : base(totalCount, items)
        {
            this.Footer = footer;
        }

        /// <summary>
        /// 页脚 信息
        /// </summary>
        public K Footer { get; set; }
    }
}
