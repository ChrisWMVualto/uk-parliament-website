using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class VideoTransforms
    {
        public static IEnumerable<EventModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if(jArray == null) return Enumerable.Empty<EventModel>();

            var list = new List<EventModel>();
            foreach(var json in jArray)
            {
                list.Add(Transform(json));
            }
            return list;
        }

        public static EventModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var id = (Guid)jObject.id;
            var planningEventState = (PlanningEventState)jObject.states.planningState;
            var recordingEventState = (RecordingEventState)jObject.states.recordingState;
            var recordedEventState = (RecordedEventState)jObject.states.recordedState;
            var actualLiveStartTime = (DateTime?)jObject.actualLiveStartTime;
            var scheduledStartTime = (DateTime)jObject.scheduledStartTime;
            var publishedStartTime = (DateTime?)jObject.publishedStartTime;
            var actualStartTime = (DateTime?)jObject.actualStartTime;

            return new EventModel(id, new EventStates(planningEventState, recordingEventState, recordedEventState), actualLiveStartTime, scheduledStartTime, publishedStartTime, actualStartTime);
        }

        public static IEnumerable<EventModel> TransformEPG(dynamic json)
        {
            dynamic jObject = JObject.Parse(json);
            if(jObject == null) return null;

            var events = TransformArray(jObject.events.ToString());

            return events;
        }

        public static VideoModel TransformVideo(dynamic json)
        {
            dynamic jObject = JObject.Parse(json);
            if(jObject == null) return null;

            var @event = Transform(jObject.@event.ToString());
            var embedCode = (string)jObject.embedCode;

            return new VideoModel(@event, embedCode);
        }
    }
}
