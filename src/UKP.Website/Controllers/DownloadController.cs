using System;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class DownloadController : Controller
    {
        private readonly IDownloadService _downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        [HttpGet]
        public virtual ActionResult Index(Guid id)
        {
            var model = _downloadService.GetDownloadUrl(id);

            if (model == null)
            {
                return View(MVC.Download.Views.DownloadExpired);
            }

            return View(model);
        }
    }
}