using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Search
{
    public class SearchViewModel
    {
        private VideoCollectionModel _results;

        public string Keywords { get; set; }
        public int? MemberId { get; set; }
        public string Business { get; set; }
        public string House { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SelectList BusinessTags { get; set; }
        public SelectList HouseTags { get; set; }
        public string SelectedBusiness { get; set; }
        public string SelectedHouse { get; set; }
        public string Member { get; set; }

        public VideoCollectionModel SearchResult
        {
            get { return _results ?? (_results = new VideoCollectionModel(Enumerable.Empty<VideoModel>(), 0, 0)); }
            set { _results = value; }
        }
    }
}