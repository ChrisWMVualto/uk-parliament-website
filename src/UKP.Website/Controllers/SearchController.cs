using System.Web.Mvc;
using UKP.Website.Models;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService _searchService)
        {
            this._searchService = _searchService;
        }

        public virtual ActionResult Index()
        {
            // TODO: Remove faked query
            SearchQueryModel searchQuery = new SearchQueryModel("1", null, null, null);

            var results = _searchService.Search(searchQuery);
            return View(results);
        }
    }
}