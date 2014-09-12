using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class EventViewModel
    {
        public EventViewModel(VideoModel videoModel)
        {
            VideoModel = videoModel;
        }

        public VideoModel VideoModel { get; private set; }
    }
}