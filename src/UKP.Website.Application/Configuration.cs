﻿using System.Configuration;

namespace UKP.Website.Application
{
    public class Configuration : IConfiguration
    {
        public string IasBaseUrl
        {
            get { return ConfigurationManager.AppSettings["IasBaseUrl"]; }
        }

        public string IasAuthKey
        {
            get { return ConfigurationManager.AppSettings["IasAuthKey"]; }
        }

        public string MemberAutocompleteApi
        {
            get { return ConfigurationManager.AppSettings["MemberAutocompleteApi"]; }
        }

        public string RssUrl
        {
            get { return ConfigurationManager.AppSettings["RssUrl"]; }
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
    }
}
