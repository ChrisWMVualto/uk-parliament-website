using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public SearchViewModel(string memberAutocompleteUrl, SearchQueryModel queryModel, IEnumerable<SearchResultsModel> searchResults = null)
        {
            SearchResults = searchResults;
            MemberAutocompleteUrl = memberAutocompleteUrl;
            QueryModel = queryModel;
        }

        public SearchViewModel()
        {
            
        }

        public string MemberAutocompleteUrl { get; set; }
        public SearchQueryModel QueryModel { get; set; }
        public IEnumerable<SearchResultsModel> SearchResults { get; set; }
    }
}