using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class DownloadResponseModel
    {
        public DownloadResponseModel(bool successful, string message, int resetHours, int resetMinutes, string email, int downloadsRemaining)
        {
            Successful = successful;
            Message = message;
            ResetHours = resetHours;
            ResetMinutes = resetMinutes;
            Email = email;
            DownloadsRemaining = downloadsRemaining;
        }

        public bool Successful { get; set; }
        public string Message { get; set; }
        public int ResetHours { get; set; }
        public int ResetMinutes { get; set; }
        public string Email { get; set; }
        public int DownloadsRemaining { get; set; }
    }
}
