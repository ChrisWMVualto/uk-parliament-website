using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class CreateDownloadModel
    {

        public Guid EventId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EmailAddress { get; set; }
        public bool AudioOnly { get; set; }
        public string StreamUrl { get; set; }

    }
}