using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class SearchResultsModel
    {
        public SearchResultsModel(EventModel @event, IEnumerable<MomentModel> moments, string pageUrl)
        {
            Event = @event;
            Moments = moments;
            PageUrl = pageUrl;
        }

        public EventModel Event { get; private set; }
        public IEnumerable<MomentModel> Moments { get; set; }
        public string PageUrl { get; private set; }
    }
}
