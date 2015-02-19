using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UKP.Website.Models;
using UKP.Website.Models.Event;
using UKP.Website.Models.Search;
using UKP.Website.Service;
using UKP.Website.Service.Model;


namespace UKP.Website.Controllers
{
    [OutputCache(Duration=600, VaryByParam="*")]
    public partial class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet]
        public virtual ActionResult Index(string keywords, int? memberId, string member, string house, string business, string start, string end, int page = 1)
        {
            var firstSearchLoad = string.IsNullOrWhiteSpace(start) && string.IsNullOrWhiteSpace(end);
            DateTime fromDate;
            DateTime toDate;

            if (firstSearchLoad)
            {
                fromDate = DateTime.Today.AddMonths(-1);
                toDate = DateTime.Today;
            }
            else
            {
                if(!DateTime.TryParse(start, out fromDate))
                {
                    ModelState.AddModelError("start", "Start date must be valid.");
                }
  
                if(!DateTime.TryParse(end, out toDate))
                {        
                    ModelState.AddModelError("end", "End date must be valid.");
                }

                if(fromDate > toDate)
                    ModelState.AddModelError("dates", "End date cannot occur before the start date.");
            }

            var searchModel = new SearchViewModel()
            {
                Keywords = keywords,
                MemberId = memberId,
                House = house,
                Business = business,
                Start = fromDate,
                End = toDate,
                Member = member,
                BusinessTags = new SelectList(_searchService.GetTags().Where(x => x.Category == "Business"), "Tag", "DisplayTag"),
                HouseTags = new SelectList(_searchService.GetTags().Where(x => x.Category == "House"), "Tag", "DisplayTag"),
                FirstSearchLoad = firstSearchLoad
            };

            if (ModelState.IsValid)
            {
                if(!firstSearchLoad)
                {
                    searchModel.SearchResult = _searchService.Search(keywords, memberId, house, business, fromDate.Date, toDate.AddDays(1).AddSeconds(1), page);
                }
            }

           
            return View(searchModel);
        }

        [HttpGet]
        public virtual PartialViewResult Moments(Guid eventId, string keywords, int? memberId)
        {
            var results = _searchService.SearchMoments(eventId, keywords, memberId);
            var searchMomentModel = new SearchMomentModel(eventId, results, 5);
            return PartialView(MVC.Search.Views._SearchMoment, searchMomentModel);
        }

        [HttpGet]
        public virtual ActionResult Members(string name)
        {
            if(name.Length <= 3) return null;
            var results = _searchService.SearchMembers(name);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}