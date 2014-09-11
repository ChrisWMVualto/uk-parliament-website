namespace UKP.Website.Service.Model
{
    public class EventStates
    {
        public EventStates(PlanningEventState planningState, RecordingEventState recordingState, RecordedEventState recordedState, SimpleEventState simpleEventState)
        {
            PlanningState = planningState;
            RecordingState = recordingState;
            RecordedState = recordedState;
            SimpleEventState = simpleEventState;
        }

        public PlanningEventState PlanningState { get; private set; }
        public RecordingEventState RecordingState { get; private set; }
        public RecordedEventState RecordedState { get; private set; }
        public SimpleEventState SimpleEventState { get; private set; }
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

    public enum SimpleEventState
    {
        PRELIVE = 0,
        LIVE = 1,
        ARCHIVE = 2,
        ERROR = 3
    }
}
