namespace AirportBroadcast.Configuration.Tenants.Dto
{
    /// <summary>
    /// 支付方式设置
    /// </summary>
    public class PayMentSettingEditDto
    {
        public AliPayDto AliPay { get; set; }

        public WxPayDto WxPay { get; set; }
        public CCBPayDto CCBPay { get; set; }
        public AliTripDto AliTrip { get; set; }

        public CfbPayDto CfbPay { get; set; }

        public string Subject { get; set; }
    }

    public class AliPayDto
    {
        /// <summary>
        /// 网关地址
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Appid
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKeyPem { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string AlipayPulicKey { get; set; }

        /// <summary>
        /// 支付宝过期时间
        /// </summary>
        public string It_b_pay { get; set; }
    }

    public class CCBPayDto
    {

        public string MERCHANTID { get; set; }


        public string POSID { get; set; }


        public string BRANCHID { get; set; }


        public string QUPWD { get; set; }


        public string USERID { get; set; }
        public string PASSWORD { get; set; }
        public string QueryUrl { get; set; }
        public string CreateUrl { get; set; }
        public string PUB { get; set; }

        public string SocketIp { get; set; }

        public string SocketPort { get; set; }
    }
    public class WxPayDto
    {
        public string NotifyUrl { get; set; }

        public string AppId { get; set; }

        public string MchId { get; set; }

        public string Key { get; set; }

        public string SslcertPath { get; set; }
    }
    public class AliTripDto
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string SessionKey { get; set; }

        public string ServiceUrl { get; set; }

        public string StartProvinceName { get; set; }
        public string StartCityName { get; set; }
        public string TradeSource { get; set; }
        public string ServiceProviderId { get; set; }

    }

    public class CfbPayDto
    {
        public string NotifyUrl { get; set; }
        public string PayUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string ReturnDesKey { get; set; }
    }
}
 