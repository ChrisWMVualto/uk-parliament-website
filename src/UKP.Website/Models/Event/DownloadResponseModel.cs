using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKP.Website.Models.Event
{
    public class DownloadResponseModel
    {
        public DownloadResponseModel(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}