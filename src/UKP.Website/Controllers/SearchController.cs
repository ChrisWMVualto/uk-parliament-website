﻿using System.Web.Mvc;
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

        public virtual ActionResult Index()
        {
            // TODO: Remove faked query
            var searchQuery = new SearchQueryModel("commons", "Commons", "Committee", 29, 30);

            var results = _searchService.Search(searchQuery);
            var model = new SearchViewModel(results, _configuration.MemberAutocompleteApi);
            return View(model);
        }
    }
}