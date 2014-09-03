using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
