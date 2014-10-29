using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class PlayerController : Controller
    {
        private readonly IVideoService _videoService;

        public PlayerController(IVideoService videoService)
        {
            _videoService = videoService;
        }


        [HttpGet]
        public virtual ActionResult LegacyPlayerRoute()
        {
            if(Request.Url != null)
            {
                var embedString = Request.QueryString.ToString().Split(' ', '+');
                var meetingId = embedString[0];

                var legacyVideo = _videoService.GetLegacyVideo(int.Parse(meetingId));
                if(legacyVideo == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                var script = string.Format("document.write('{0}');", legacyVideo.EmbedCode);
                return JavaScript(script);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}