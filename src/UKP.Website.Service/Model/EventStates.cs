namespace UKP.Website.Service.Model
{
    public class EventStates
    {
        public EventStates(PlanningEventState planningState, RecordingEventState recordingState, RecordedEventState recordedState, PlayerEventState playerState)
        {
            PlanningState = planningState;
            RecordingState = recordingState;
            RecordedState = recordedState;
            PlayerState = playerState;
        }

        public PlanningEventState PlanningState { get; private set; }
        public RecordingEventState RecordingState { get; private set; }
        public RecordedEventState RecordedState { get; private set; }
        public PlayerEventState PlayerState { get; private set; }
    }

    public enum PlanningEventState
    {
        VOID = 0,
        NEW = 1,
        PROPOSED = 2,
        CONFIRMED = 3,
        STOP_DVR = 4
    }

    public enum RecordingEventState
    {
        VOID = 0,
        IDLE = 1,
        ERROR = 2,
        RECORDING = 3,
        COMPLETED = 4
    }

    public enum RecordedEventState
    {
        VOID = 0,
        NEW = 1,
        REVOKE = 2,
        HOLD = 3,
        BASIC = 4
    }

    public enum PlayerEventState
    {
        PRELIVE = 0,
        LIVE = 1,
        ARCHIVE = 2,
        ERROR = 3
    }
}
