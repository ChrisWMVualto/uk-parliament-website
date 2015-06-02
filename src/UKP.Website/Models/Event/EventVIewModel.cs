using System.Collections.Generic;
using System.Linq;
using System.Web;
using UKP.Website.Application;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class EventViewModel
    {
        public EventViewModel(VideoModel videoModel, bool defaultToStackTab)
        {
            VideoModel = videoModel;
            DefaultToStackTab = defaultToStackTab;
        }

        public static bool ShowAudioOnly(VideoModel video)
        {
            return (!video.Event.LegacyMeetingId.HasValue || video.Event.ProductionSource != ProductionSourceConstants.AUDIO)
                   && (video.Event.States.PlayerState == PlayerEventState.LIVE || video.Event.States.PlayerState == PlayerEventState.ARCHIVE)
                   && video.Event.PublishedStartTime.HasValue;
        }

        public VideoModel VideoModel { get; private set; }
        public bool DefaultToStackTab { get; private set; }
        public bool IsClipped { get { return VideoModel.RequestedInPoint.HasValue && VideoModel.RequestedOutPoint.HasValue; } }
    }
}