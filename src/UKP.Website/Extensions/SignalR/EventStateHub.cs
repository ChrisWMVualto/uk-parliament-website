using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using UKP.Website.Service.Model;

namespace UKP.Website.Extensions.SignalR
{
    public class EventStateHub : Hub
    {
        private static readonly List<PlanningEventState> InterestedPlanningStates = new List<PlanningEventState>()
                                                          {
                                                              PlanningEventState.STOP_DVR,
                                                              PlanningEventState.CONFIRMED
                                                          };

        private static readonly List<RecordingEventState> InterestedRecordingStates = new List<RecordingEventState>()
                                                          {
                                                              RecordingEventState.RECORDING,
                                                              RecordingEventState.COMPLETED
                                                          };

        private static readonly List<RecordedEventState> InterestedRecordedStates = new List<RecordedEventState>()
                                                          {
                                                              RecordedEventState.BASIC,
                                                              RecordedEventState.HOLD,
                                                              RecordedEventState.REVOKE
                                                          };


        public static void EventStateChanged(Guid id, EventStates states, bool stateChanged)
        {
            if(!stateChanged) return;

            if(InterestedPlanningStates.Contains(states.PlanningState)
                || InterestedRecordingStates.Contains(states.RecordingState)
                || InterestedRecordedStates.Contains(states.RecordedState)) ;
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<EventStateHub>();
                context.Clients.All.eventStateChanged(id, states.PlanningState.ToString(), states.RecordingState.ToString(), states.RecordedState.ToString(), states.SimpleEventState.ToString());
            }
        }
    }
}