using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class EventStates
    {
        public EventStates(PlanningEventState planningState, RecordingEventState recordingState, RecordedEventState recordedState)
        {
            PlanningState = planningState;
            RecordingState = recordingState;
            RecordedState = recordedState;
        }

        public PlanningEventState PlanningState { get; private set; }
        public RecordingEventState RecordingState { get; private set; }
        public RecordedEventState RecordedState { get; private set; }
    }

    public enum PlanningEventState
    {
        VOID,
        NEW,
        PROPOSED,
        CONFIRMED,
        STOP_DVR
    }

    public enum RecordingEventState
    {
        VOID,
        IDLE,
        ERROR,
        RECORDING,
        COMPLETED
    }

    public enum RecordedEventState
    {
        VOID,
        NEW,
        REVOKE,
        HOLD,
        BASIC
    }
}
