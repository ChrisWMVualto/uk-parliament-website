using System;
using System.Collections.Generic;
using UKP.Website.Application;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Guide
{
    public class GuideViewModel
    {
        public GuideViewModel(IEnumerable<EpgChannelModel> events, DateTime date, IEnumerable<ChannelModel> channels)
        {
            Events = events;
            BaseDate = date;
            Channels = channels;
        }

        public GuideViewModel(IEnumerable<EpgChannelModel> events, IEnumerable<ChannelModel> channels)
            : this(events, DateTime.Now, channels)
        {
            
        }

        public int LivePosition()
        {
            var timespan = DateTime.Now.ToLocalTime().Subtract(DateTime.Today);
            return Convert.ToInt32(timespan.TotalMinutes * EventConstants.EPG_MINUTE_SIZE);
        }

        public IEnumerable<EpgChannelModel> Events { get; private set; }
        public DateTime BaseDate { get; private set; }
        public IEnumerable<ChannelModel> Channels { get; private set; }
    }
}