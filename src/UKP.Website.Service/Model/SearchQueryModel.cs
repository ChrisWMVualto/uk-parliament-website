using System;

namespace UKP.Website.Service.Model
{
    public class SearchQueryModel
    {
        public string Keywords { get; set; }
        public string Tags { get; set; }
        public int? MemberId { get; set; }
        public DateTime Period { get; set; }

        public SearchQueryModel(string keywords, string tags, int? memberId, int period)
        {
            Keywords = keywords;
            Tags = tags;
            MemberId = memberId;
            Period = DateTime.Today.AddDays(period * -1);
        }
    }
}