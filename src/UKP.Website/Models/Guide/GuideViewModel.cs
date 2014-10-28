using System;
using System.Collections.Generic;
using UKP.Website.Application;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Guide
{
    public class GuideViewModel
    {
        public List<EpgChannelModel> Events { get; private set; }
        public DateTime BaseDate { get; private set; }

        public GuideViewModel(List<EpgChannelModel> events)
        {
            Events = events;
            BaseDate = DateTime.Today;
        }

        public string LivePosition()
        {
            var timespan = DateTime.Now.ToLocalTime().Subtract(DateTime.Today);
            return String.Format("{0}px", (timespan.TotalMinutes * EventConstants.EPG_MINUTE_SIZE));
        }
    }
}