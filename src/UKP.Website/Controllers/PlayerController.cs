﻿using System;
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
            //TODO: add support for in point timecode
            if (Request.Url != null)
            {
                var decodedUrl = HttpUtility.UrlDecode(Request.Url.Query);
                if(decodedUrl != null)
                {
                    var embedString = decodedUrl.Replace("?", "").Split(new[] { ' ' });
                    var meetingId = embedString[0];
                    var legacyVideo = _videoService.GetLegacyVideo(int.Parse(meetingId));
                    return JavaScript(legacyVideo.LegacyEmbedCode);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}