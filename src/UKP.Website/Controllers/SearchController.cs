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
        public virtual ActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
                return View(new SearchViewModel(_configuration.MemberAutocompleteApi, model.QueryModel));

            var results = _searchService.Search(model.QueryModel);
            var response = new SearchViewModel(_configuration.MemberAutocompleteApi, model.QueryModel, results);
            return View(response);
        }
    }
}