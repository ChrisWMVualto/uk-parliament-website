﻿using System.Web.Mvc;
using System.Web.Services;
using UKP.Website.Service;
using UKP.Website.Service.Model;


namespace UKP.Website.Controllers
{
    public partial class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IMemberService _memberService;

        public SearchController(ISearchService searchService, IMemberService memberService)
        {
            _searchService = searchService;
            _memberService = memberService;
        }

        public virtual ActionResult Index()
        {
            // TODO: Remove faked query
            var searchQuery = new SearchQueryModel("commons", "Commons, Lords", 29, 30);

            var results = _searchService.Search(searchQuery);
            return View(results);
        }

        [WebMethod]
        public string Member(string query)
        {
            var results = _memberService.Search(query);
            return results;
        }
    }
}