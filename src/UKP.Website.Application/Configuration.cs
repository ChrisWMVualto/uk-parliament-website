using System.Configuration;

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
    }
}
