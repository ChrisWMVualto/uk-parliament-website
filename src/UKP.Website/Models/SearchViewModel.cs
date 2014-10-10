using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public SearchViewModel(string memberAutocompleteUrl, SearchFormModel formModel, VideoCollectionModel videoCollectionResult = null)
        {
            VideoCollectionResult = videoCollectionResult;
            MemberAutocompleteUrl = memberAutocompleteUrl;
            FormModel = formModel;
        }

        public SearchViewModel()
        {
            
        }

        public string MemberAutocompleteUrl { get; set; }
        public SearchFormModel FormModel { get; set; }
        public VideoCollectionModel VideoCollectionResult { get; set; }
    }
}