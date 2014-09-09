using System.Web.Mvc;
using UKP.Website.Service;
using UKP.Website.Service.Model;

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
            var searchQuery = new SearchQueryModel("commons", "Commons, Lords", 29, null);

            var results = _searchService.Search(searchQuery);
            return View(results);
        }
    }
}