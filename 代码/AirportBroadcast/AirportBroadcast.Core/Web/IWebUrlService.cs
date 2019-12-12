namespace AirportBroadcast.Web
{
    public interface IWebUrlService
    {
        string GetSiteRootAddress(string tenancyName = null);
    }
}
