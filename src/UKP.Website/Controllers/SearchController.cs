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

        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = new SearchViewModel(_configuration.MemberAutocompleteApi, new SearchQueryModel());
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Index(SearchQueryModel searchQuery)
        {
            var results = _searchService.Search(searchQuery);
            var model = new SearchViewModel(_configuration.MemberAutocompleteApi, searchQuery, results);
            return View(model);
        }
    }
}