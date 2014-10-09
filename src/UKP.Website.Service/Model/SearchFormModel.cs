﻿using System;

namespace UKP.Website.Service.Model
{
    public class SearchFormModel
    {
        public string Business { get; set; }
        public string House { get; set; }

        public string Keywords { get; set; }
        public int? MemberId { get; set; }
        public DateTime Period { get; set; }

        public SearchFormModel(string keywords, string house, string business, int? memberId, int period)
        {
            Business = business;
            House = house;
            Keywords = keywords;
            MemberId = memberId;
            Period = DateTime.Today.AddDays(period * -1);
        }

        public SearchFormModel()
        {
            
        }
    }
}