using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class EventTransforms
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
            var title = (string) jObject.title;
            var house = (string)jObject.house;
            var business = (string)jObject.business;
            var planningEventState = (PlanningEventState)jObject.states.planningState;
            var recordingEventState = (RecordingEventState)jObject.states.recordingState;
            var recordedEventState = (RecordedEventState)jObject.states.recordedState;
            var playerEventState = (PlayerEventState)jObject.states.playerState;
            var displayStartDate = (DateTime)jObject.displayStartTime;
            var displayEndDate = (DateTime)jObject.displayEndTime;
            var actualLiveStartTime = (DateTime?)jObject.actualLiveStartTime;
            var scheduledStartTime = (DateTime)jObject.scheduledStartTime;
            var scheduledEndTime = (DateTime)jObject.scheduledEndTime;
            var publishedStartTime = (DateTime?)jObject.publishedStartTime;
            var publishedEndTime = (DateTime?)jObject.publishedEndTime;
            var actualStartTime = (DateTime?)jObject.actualStartTime;
            var actualEndTime = (DateTime?)jObject.actualEndTime;
            var thumbnailUrl = (string) jObject.thumbnailUrl;
            var channelName = (string)jObject.channelName;
            var room = (string)jObject.room;
            var eventType = (string)jObject.eventType;
            var productionSource = (string)jObject.productionSource;

            return new EventModel(id, title, house, business, new EventStates(planningEventState, recordingEventState, recordedEventState, playerEventState), displayStartDate, displayEndDate, actualLiveStartTime,
                scheduledStartTime, scheduledEndTime, publishedStartTime, publishedEndTime, actualStartTime, actualEndTime, thumbnailUrl, channelName, room, eventType, productionSource);
        }

        public static IEnumerable<EventModel> TransformEPG(dynamic json)
        {
            dynamic jObject = JObject.Parse(json);
            if(jObject == null) return null;

            var events = TransformArray(jObject.events.ToString());

            return events;
        }
    }
}
