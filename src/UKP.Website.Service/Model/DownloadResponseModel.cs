using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class DownloadResponseModel
    {
        public DownloadResponseModel(bool successful, string message)
        {
            Successful = successful;
            Message = message;
        }

        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}
