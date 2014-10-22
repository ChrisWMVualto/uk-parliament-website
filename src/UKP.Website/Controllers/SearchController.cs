using System;
using System.Net;
using System.Web.Mvc;
using UKP.Website.Application;
using UKP.Website.Extensions;
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
        public virtual ActionResult Index(SearchViewModel model = null, int? pageNum = null)
        {
            if (model.FormModel == null)
            {
                return View(new SearchViewModel(new SearchFormModel()));
            }

            if (!ModelState.IsValid)
                return View(model);

            var results = _searchService.Search(model.FormModel.Keywords, model.FormModel.MemberId, model.FormModel.House, model.FormModel.Business, model.FormModel.StartDate, model.FormModel.EndDate, pageNum.HasValue ? pageNum.Value : 1);
            var response = new SearchViewModel(model.FormModel, results);
            return View(response);
        }

        [HttpGet]
        public virtual PartialViewResult Moments(SearchViewModel model, string eventId, int skipItems = 5)
        {
            if (model.FormModel == null || !ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return null;
            }

            var results = _searchService.SearchMoments(eventId, model.FormModel.Keywords, model.FormModel.MemberId, model.FormModel.House, model.FormModel.Business, skipItems);
            var @event = new EventModel(Guid.Parse(eventId));
            var resultModel = new VideoModel(@event, results);

            return PartialView(MVC.Search.Views._SearchMoment, resultModel);
        }
    }
}