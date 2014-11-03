using System;

namespace UKP.Website.Application
{
    public interface IConfiguration
    {
        string IasBaseUrl { get; }
        string IasAuthKey { get; }
        string RssUrl { get; }
        DateTime EpgStartDate { get; }
    }
}