namespace AirportBroadcast.Configuration.Tenants.Dto
{
    public class PushDataSettingsEditDto
    {
        

        public string OnLineUrl { get; set; }

        public string PrivateKey { get; set; }
         
        public string StartDate { get; set; }

        public string PushMsgInfoUrl { get; set; }

        public string PushMsgDesKey { get; set; }

        public string WxTemplateCode { get; set; }

        public string AliTemplateCode { get; set; }

        public string LogMaxId { get; set; }
    }
}
