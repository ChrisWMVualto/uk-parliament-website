using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKP.Website.Models.Event
{
    public class CaptchaVerifyModel
    {
        public string Response { get; set; }

        public CaptchaVerifyModel(string response)
        {
            Response = response;
        }
    }
}