using System.Collections.Generic;
using UKP.Website.Models;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface ISearchService
    {
        IEnumerable<SearchModel> Search(SearchQueryModel search);
    }
}
