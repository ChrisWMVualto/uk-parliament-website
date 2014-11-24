using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class StateChangeModel
    {
        public Guid EventId { get; set; }
        public PlanningEventState PlanningState { get; set; }
        public RecordingEventState RecordingState { get; set; }
        public RecordedEventState RecordedState { get; set; }
        public PlayerEventState PlayerState { get; set; }
        public bool StateChanged { get; set; }
    }
}