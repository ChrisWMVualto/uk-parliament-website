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

        private int TemplateWidthInt()
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
            var difference = EventData.DisplayStartDate.ToLocalTime().Subtract(EventData.DisplayStartDate.ToLocalTime().Date);
            var left = EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;

            return string.Format("{0}px", left);
        }

        public bool SmallItem()
        {
            return TemplateWidthInt() <= (90 * EventConstants.EPG_MINUTE_SIZE);
        }
    }
}
