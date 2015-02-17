using System;
using UKP.Website.Application;

namespace UKP.Website.Service.Model
{
    public class EpgEventModel
    {
        public EventModel EventData { get; set; }
        public DateTime BaseDate { get; set; }

        public EpgEventModel(EventModel eventData, DateTime baseDate)
        {
            EventData = eventData;
            BaseDate = baseDate;
        }

        public EpgEventModel()
        {
            
        }

        public int TemplateWidthInt()
        {
            var difference = EventData.DisplayEndDate.Subtract(EventData.DisplayStartDate);
            return EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;
        }

        public string TemplateWidth()
        {
            return string.Format("{0}px", TemplateWidthInt());
        }

        public string TemplateLeftPosition()
        {
            TimeSpan difference;

            if (EventData.DisplayStartDate > BaseDate.Date)
                difference = EventData.DisplayStartDate.ToLocalTime().Subtract(EventData.DisplayStartDate.ToLocalTime().Date);
            else
                difference = EventData.DisplayStartDate.ToLocalTime().Subtract(BaseDate.ToLocalTime().Date);

            var left = EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;

            return string.Format("{0}px", left);
        }
    }
}
