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
            DateTime fromDate;
            if (!DateTime.TryParse(start, out fromDate))
            {
                fromDate = DateTime.Today.AddMonths(-1);
                //ModelState.AddModelError("start", "Invalid start date.");
                //return RedirectToAction(MVC.Search.Index(keywords, memberId, member, house, business, fromDate.ToShortDateString(), end, page));
            }

            DateTime toDate;
            if (!DateTime.TryParse(end, out toDate))
            {
                toDate = DateTime.Today;
                //ModelState.AddModelError("end", "Invalid end date.");
                //return RedirectToAction(MVC.Search.Index(keywords, memberId, member, house, business, fromDate.ToShortDateString(), toDate.ToShortDateString(), page));
            }

            if (fromDate > toDate)
                ModelState.AddModelError("end", "End date cannot occur before the start date.");

            var firstSearchLoad = string.IsNullOrWhiteSpace(start);
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

            if(!firstSearchLoad)
            {
                searchModel.SearchResult = _searchService.Search(keywords, memberId, house, business, fromDate.Date, toDate.AddDays(1).AddSeconds(1), page);
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