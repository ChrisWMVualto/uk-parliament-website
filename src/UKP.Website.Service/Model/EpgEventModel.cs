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

        public int TemplateWidth()
        {
            var difference = EventData.DisplayEndDate.Subtract(EventData.DisplayStartDate);
            return EventConstants.EPG_MINUTE_SIZE * (int)difference.TotalMinutes;
        }
    }
}
