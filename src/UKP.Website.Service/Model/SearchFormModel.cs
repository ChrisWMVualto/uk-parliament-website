using System;
using System.ComponentModel.DataAnnotations;

namespace UKP.Website.Service.Model
{
    public class SearchFormModel
    {
        public string Business { get; set; }
        public string House { get; set; }

        public string Keywords { get; set; }
        public int? MemberId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public SearchFormModel(string keywords, string house, string business, int? memberId, DateTime? start, DateTime? end)
        {
            Business = business;
            House = house;
            Keywords = keywords;
            MemberId = memberId;
            StartDate = start.HasValue ? start.Value : DateTime.Now;
            EndDate = end.HasValue ? end.Value : DateTime.Now.AddMonths(-1);
        }

        public SearchFormModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddMonths(-1);
        }
    }
}