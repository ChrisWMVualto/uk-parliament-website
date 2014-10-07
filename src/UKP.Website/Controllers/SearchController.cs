using System;
using System.Web.Mvc;
using System.Web.Services;
using UKP.Website.Application;
using UKP.Website.Models;
using UKP.Website.Service;
using UKP.Website.Service.Model;


namespace UKP.Website.Controllers
{
    public partial class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IConfiguration _configuration;

        public SearchController(ISearchService searchService, IConfiguration configuration)
        {
            _searchService = searchService;
            _configuration = configuration;
        }

        [HttpGet]
        public virtual ActionResult Index(SearchViewModel model = null, int pageNum = 1)
        {
            if (model == null)
            {
                return View(new SearchViewModel(_configuration.MemberAutocompleteApi, new SearchFormModel()));
            }

            if (!ModelState.IsValid)
                return View(new SearchViewModel(_configuration.MemberAutocompleteApi, model.FormModel));

            var results = _searchService.Search(model.FormModel.Keywords, model.FormModel.MemberId, model.FormModel.House, model.FormModel.Business, model.FormModel.Period, pageNum);
            var response = new SearchViewModel(_configuration.MemberAutocompleteApi, model.FormModel, results);
            return View(response);
        }
    }
}