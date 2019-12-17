using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 催促登机
    /// </summary>
    public class URGCommand
    {
        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }

        /// <summary>
        /// 催促信息
        /// </summary>
        public virtual UrgeInfo UrgeInfo { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

            //解析定位条件 ：
            //FLNU，服务所属航班的URNO（不足10位左边补空格） 
            //FLNO，所属航班的航班号（不足9位左边补空格）
            //STOA或STOD，所属航班的计划时间  14位 
            //进出港标志 A 表示进港，D表示出港 1位 
            //SRNU，服务类型 具体服务有值机服务、登机服务、上轮档服务 5位
            var condition = data.Substring(0, 10 + 9 + 14 + 1 + 5);

            UrgeInfo = new UrgeInfo();
            UrgeInfo.URNO = condition.Substring(0, 10).Trim();//航班的URNO 10位
            UrgeInfo.FLNO = condition.Substring(10, 9).Trim();//FLNO 航班号 9位 
            UrgeInfo.AORD = condition.Substring(10 + 9 + 14, 1);//A 表示进港，D表示出港 1位 
            var time = condition.Substring(10 + 9, 14);// STOA或STOD，所属航班的计划时间  14位  YYYYMMDDhhmmss 

            if (UrgeInfo.AORD == "A")
            {
                //UrgeInfo.STOA = time;
            }
            else
            {
                UrgeInfo.STOD = time;
            }
            data = data.Substring(10 + 9 + 14 + 1);//去掉定位条件

            UrgeInfo.UrgeDatas = new Dictionary<string, string>();

            var fieldValues = data.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < fieldValues.Length; i += 2)//催促内容解析
            {
                UrgeInfo.UrgeDatas.Add(fieldValues[i], fieldValues[i + 1]);
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
