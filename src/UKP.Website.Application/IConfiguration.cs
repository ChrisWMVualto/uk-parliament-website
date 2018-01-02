using System;

namespace UKP.Website.Application
{
    public interface IConfiguration
    {
        string IasBaseUrl { get; }
        string IasAuthKey { get; }
        string RssUrl { get; }
        DateTime EpgStartDate { get; }
        string GetMemberAutocompleteApi { get; }
        bool RobotsAllow { get; }
        string GoogleRecaptchaVerifyUrl { get; }
        string RecaptchaSecret { get; }
    }
}