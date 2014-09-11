﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var title = (string) jObject.title;
            var house = (string)jObject.house;
            var business = (string)jObject.business;
            var planningEventState = (PlanningEventState)jObject.states.planningState;
            var recordingEventState = (RecordingEventState)jObject.states.recordingState;
            var recordedEventState = (RecordedEventState)jObject.states.recordedState;
            var simpleEventState = (SimpleEventState)jObject.states.simpleState;
            var actualLiveStartTime = (DateTime?)jObject.actualLiveStartTime;
            var scheduledStartTime = (DateTime)jObject.scheduledStartTime;
            var publishedStartTime = (DateTime?)jObject.publishedStartTime;
            var actualStartTime = (DateTime?)jObject.actualStartTime;

            return new EventModel(id, title, house, business, new EventStates(planningEventState, recordingEventState, recordedEventState, simpleEventState), actualLiveStartTime,
                scheduledStartTime, publishedStartTime, actualStartTime);
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
            var legacyEmbedCode = (string)jObject.legacyEmbedCode;

            return new VideoModel(@event, embedCode, legacyEmbedCode);
        }
    }
}
