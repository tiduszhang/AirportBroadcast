using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportBroadcast.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static  class PinYinHelper
    {
        public static string MkPinyinString(string ChsStr)
        {
            string returnStr = "";
            for (int i = 0; i < ChsStr.Length; i++)
            {
                string tmpStr = GetPinyin(ChsStr[i]);
                if (tmpStr.Length > 0)
                {
                    if (returnStr != "")
                    {
                        Regex regex = new Regex(",");
                        string[] tmpArr = regex.Split(returnStr);
                        returnStr = "";
                        for (int j = 0; j < tmpArr.Length; j++)
                        {
                            for (int k = 0; k < tmpStr.Length; k++)
                            {
                                string charcode = tmpStr[k].ToString();
                                returnStr = returnStr + tmpArr[j] + charcode + ",";
                            }
                        }
                        if (returnStr != "")
                        {
                            returnStr = returnStr.Substring(0, returnStr.Length - 1);
                        }
                    }
                    else
                    {
                        for (int l = 0; l < tmpStr.Length - 1; l++)
                        {
                            returnStr = returnStr + tmpStr[l] + ",";
                        }
                        returnStr += tmpStr[tmpStr.Length - 1];
                    }
                }
            }
            return returnStr;
        }

        public static string GetPinyin(char c)
        {
            String firstPinyin = "";
            int uni = (int)c;
            if (uni > 40869 || uni < 19968)
            {
                return firstPinyin;
            }
            ChineseChar chineseChar = new ChineseChar(c);

            //因为一个汉字可能有多个读音，pinyins是一个集合
            var pinyins = chineseChar.Pinyins;
           
            //下面的方法只是简单的获得了集合中第一个非空元素
            foreach (var pinyin in pinyins)
            {
                if (pinyin != null)
                {
                    //拼音的最后一个字符是音调
                    var tc = pinyin.Substring(0, 1);
                    if (!firstPinyin.Contains(tc))
                        firstPinyin += tc;
                }
            }
            return firstPinyin;
        }

    }
}
