using System;
using System.Configuration;

namespace UKP.Website.Application
{
    public class Configuration : IConfiguration
    {
        public string IasBaseUrl
        {
            get { return ConfigurationManager.AppSettings["IasBaseUrl"]; }
        }

        public static string GetIasBaseUrl
        {
            get { return ConfigurationManager.AppSettings["IasBaseUrl"]; }
        }

        public string IasAuthKey
        {
            get { return ConfigurationManager.AppSettings["IasAuthKey"]; }
        }

        public static string UKPHelpUrl { get { return ConfigurationManager.AppSettings["UKPHelpUrl"]; } }
        
        public string RecaptchaSecret { get { return ConfigurationManager.AppSettings["RecaptchaSecret"]; } }

        public string GetMemberAutocompleteApi
        {
            get { return ConfigurationManager.AppSettings["MemberAutocompleteApi"]; }
        }

        public string RssUrl
        {
            get { return ConfigurationManager.AppSettings["RssUrl"]; }
        }

        public DateTime EpgStartDate
        {
            get { return DateTime.Parse(ConfigurationManager.AppSettings["EPGStartDate"]); }
        }

        public static DateTime GetEpgStartDate
        {
            get { return DateTime.Parse(ConfigurationManager.AppSettings["EPGStartDate"]); }
        }

        public static string GetRssUrl
        {
            get { return ConfigurationManager.AppSettings["RssUrl"]; }
        }

        public static string GetShareTwitter
        {
            get { return ConfigurationManager.AppSettings["share-twitter"]; }
        }

        public static string GetShareFacebook
        {
            get { return ConfigurationManager.AppSettings["share-facebook"]; }
        }

        public static string GetShareGooglePlus
        {
            get { return ConfigurationManager.AppSettings["share-google-plus"]; }
        }

        public static string GetShareLinkedIn
        {
            get { return ConfigurationManager.AppSettings["share-linkedin"]; }
        }

        public static string GetGoogleAnalyticsId
        {
            get { return ConfigurationManager.AppSettings["GoogleAnalyticsId"]; }
        }

        public bool RobotsAllow
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["RobotsAllow"]); }
        }

        public string GoogleRecaptchaVerifyUrl
        {
            get { return ConfigurationManager.AppSettings["GoogleRecaptchaVerifyUrl"]; }
        }
    }
}
