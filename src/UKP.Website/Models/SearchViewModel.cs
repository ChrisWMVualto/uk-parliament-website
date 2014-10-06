using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public SearchViewModel(string memberAutocompleteUrl, SearchFormModel formModel, SearchModel searchResults = null)
        {
            SearchResults = searchResults;
            MemberAutocompleteUrl = memberAutocompleteUrl;
            FormModel = formModel;
        }

        public SearchViewModel()
        {
            
        }

        public string MemberAutocompleteUrl { get; set; }
        public SearchFormModel FormModel { get; set; }
        public SearchModel SearchResults { get; set; }
    }
}