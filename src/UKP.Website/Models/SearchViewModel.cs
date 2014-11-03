using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public VideoCollectionModel VideoCollectionResult { get; set; }

        public string Business { get; set; }
        public string House { get; set; }

        public string Keywords { get; set; }
        public int? MemberId { get; set; }

        public int? Page { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

    }
}