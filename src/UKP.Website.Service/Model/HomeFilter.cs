namespace UKP.Website.Service.Model
{
    public class HomeFilter
    {
        private readonly EventStates _states;

        public HomeFilter(EventStates states)
        {
            _states = states;
        }

        public bool Live
        {
            get
            {
                return (_states.PlanningState == PlanningEventState.CONFIRMED || _states.PlanningState == PlanningEventState.STOP_DVR)
                    && (_states.RecordingState == RecordingEventState.RECORDING || _states.RecordingState == RecordingEventState.COMPLETED)
                    && (_states.RecordedState == RecordedEventState.NEW || _states.RecordedState == RecordedEventState.HOLD);
            }
        }



        public bool LiveAndArchive
        {
            get
            {
                return (_states.PlanningState == PlanningEventState.CONFIRMED || _states.PlanningState == PlanningEventState.STOP_DVR)
                    && (_states.RecordingState == RecordingEventState.RECORDING || _states.RecordingState == RecordingEventState.COMPLETED)
                    && (_states.RecordedState == RecordedEventState.NEW || _states.RecordedState == RecordedEventState.HOLD || _states.RecordedState == RecordedEventState.BASIC);
            }
        }

        public bool Next
        {
            get
            {
                return _states.PlanningState == PlanningEventState.CONFIRMED &&
                       _states.RecordingState == RecordingEventState.IDLE &&
                       _states.RecordedState == RecordedEventState.NEW;
            }
        }
    }
}
