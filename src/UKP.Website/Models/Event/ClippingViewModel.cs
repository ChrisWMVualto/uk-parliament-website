using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class ClippingViewModel
    {
        public ClippingViewModel(VideoModel videoModel, TimeSpan? start, TimeSpan? end)
        {
            VideoModel = videoModel;
            Start = start;
            End = end;
        }

        public VideoModel VideoModel { get; private set; }
        public TimeSpan? Start { get; private set; }
        public TimeSpan? End { get; private set; }
    }
}