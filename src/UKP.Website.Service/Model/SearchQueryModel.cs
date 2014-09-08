using System;
using Date.Extensions;
using RestSharp.Extensions;

namespace UKP.Website.Models
{
    public class SearchQueryModel
    {
        public string Keywords { get; set; }
        public string[] Tags { get; set; }
        public string[] MemberIds { get; set; }
        public DateTime? Period { get; set; }

        public SearchQueryModel(string keywords, string tags, string memberIds, string period)
        {
            Keywords = keywords;
            Tags = tags.HasValue() ? tags.Split(',') : null;
            MemberIds = memberIds.HasValue() ? memberIds.Split(',') : null;
            Period = period.FromISO8601String();
        }
    }
}