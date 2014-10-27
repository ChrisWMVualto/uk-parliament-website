using System;
using UKP.Website.Application;

namespace UKP.Website.Service.Model
{
    public class EpgEventModel
    {
        public EventModel EventData { get; set; }

        public EpgEventModel(EventModel eventData)
        {
            EventData = eventData;
        }

        public EpgEventModel()
        {
            
        }

        public string TemplateWidth()
        {
            var difference = EventData.DisplayEndDate.Subtract(EventData.DisplayStartDate);
            var width = EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;

            return string.Format("{0}px", width);
        }

        public string TemplateLeftPosition()
        {
            var difference = EventData.DisplayStartDate.ToLocalTime().Subtract(EventData.DisplayStartDate.ToLocalTime().Date);
            var left = EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;

            return string.Format("{0}px", left);
        }
    }
}
