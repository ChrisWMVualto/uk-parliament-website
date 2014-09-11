using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public SearchViewModel(IEnumerable<SearchModel> searchResults, string memberAutocompleteUrl)
        {
            SearchResults = searchResults;
            MemberAutocompleteUrl = memberAutocompleteUrl;
        }

        public string MemberAutocompleteUrl { get; set; }
        public IEnumerable<SearchModel> SearchResults { get; set; }
    }
}