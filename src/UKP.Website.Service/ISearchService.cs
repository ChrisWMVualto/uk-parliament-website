using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface ISearchService
    {
        IEnumerable<SearchResultsModel> Search(SearchQueryModel search);
    }
}
