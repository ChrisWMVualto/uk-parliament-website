﻿using System;
using System.Web.Mvc;
using UKP.Website.Models;
using UKP.Website.Models.Event;
using UKP.Website.Models.Search;
using UKP.Website.Service;
using UKP.Website.Service.Model;


namespace UKP.Website.Controllers
{
    public partial class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet]
        public virtual ActionResult Index(string keywords, int? memberId, string house, string business, string start, string end, int page = 1)
        {
            DateTime fromDate;
            if(!DateTime.TryParse(start, out fromDate))
            {
                fromDate = DateTime.Now.AddMonths(-1);
            }
            DateTime toDate;
            if(!DateTime.TryParse(end, out toDate))
            {
                toDate = DateTime.Today;
            }


            var searchResults = _searchService.Search(keywords, memberId, house, business, fromDate, toDate, page);

            var searchModel = new SearchViewModel()
                              {
                                  Keywords = keywords,
                                  MemberId = memberId,
                                  House = house,
                                  Business = business,
                                  Start = fromDate,
                                  End = toDate,
                                  SearchResult = searchResults
                              };
            return View(searchModel);
        }

        [HttpGet]
        public virtual PartialViewResult Moments(Guid eventId, string keywords, int? memberId, string house, string business)
        {
            var results = _searchService.SearchMoments(eventId, keywords, memberId, house, business);
            var searchMomentModel = new SearchMomentModel(eventId, results, 5);
            return PartialView(MVC.Search.Views._SearchMoment, searchMomentModel);
        }
    }
}