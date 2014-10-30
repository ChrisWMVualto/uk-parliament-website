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
                return (_states.PlanningState.Equals(PlanningEventState.CONFIRMED) || _states.PlanningState.Equals(PlanningEventState.STOP_DVR)) &&
                       _states.RecordedState.Equals(RecordedEventState.NEW) &&
                       (_states.RecordingState.Equals(RecordingEventState.RECORDING) || _states.RecordingState.Equals(RecordingEventState.COMPLETED));
            }
        }

        public bool Next
        {
            get
            {
                return _states.PlanningState.Equals(PlanningEventState.CONFIRMED) &&
                       _states.RecordingState.Equals(RecordingEventState.IDLE) &&
                       _states.RecordedState.Equals(RecordedEventState.NEW);
            }
        }

        public bool Archived
        {
            get
            {
                return (_states.PlanningState.Equals(PlanningEventState.CONFIRMED) || _states.PlanningState.Equals(PlanningEventState.STOP_DVR)) &&
                       _states.RecordingState.Equals(RecordingEventState.COMPLETED) &&
                       _states.RecordedState.Equals(RecordedEventState.BASIC);
            }
        }
    }
}
