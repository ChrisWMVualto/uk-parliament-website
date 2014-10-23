using System;
using System.Web.Mvc;
using UKP.Website.Models;
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
        public virtual ActionResult Index(SearchFormModel model = null, int? pageNum = null)
        {
            if (model == null)
            {
                //return View(new SearchViewModel(new SearchFormModel()));
                return View(new SearchFormModel());
            }

            if (!ModelState.IsValid)
                return View(model);

            model.Results = _searchService.Search(model.Keywords, model.MemberId, model.House, model.Business, model.StartDate, model.EndDate, pageNum.HasValue ? pageNum.Value : 1);
            return View(model);
        }

        [HttpGet]
        public virtual PartialViewResult Moments(SearchFormModel model, string eventId)
        {
            if (model == null || !ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return null;
            }

            var results = _searchService.SearchMoments(eventId, model.Keywords, model.MemberId, model.House, model.Business);
            var @event = new EventModel(Guid.Parse(eventId));
            var resultModel = new VideoModel(@event, results);

            return PartialView(MVC.Search.Views._SearchMoment, resultModel);
        }
    }
}