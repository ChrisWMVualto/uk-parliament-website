﻿using System;

namespace UKP.Website.Service.Model
{
    public class EventModel
    {
        public EventModel(Guid id, string title, string house, string business, EventStates states, DateTime displayStartDate, DateTime displayEndDate,
            DateTime? actualLiveStartTime, DateTime scheduledStartTime, DateTime scheduledEndTime, DateTime? publishedStartTime,
            DateTime? actualStartTime, DateTime? actualEndTime, string channelName, int? internalChannelId, string room, string eventType, string productionSource, int? legacyMeetingId,
            string externalLocation, string subTitle)
        {
            Id = id;
            Title = title;
            House = house;
            Business = business;
            States = states;
            DisplayStartDate = displayStartDate;
            DisplayEndDate = displayEndDate;
            ActualLiveStartTime = actualLiveStartTime;
            ScheduledStartTime = scheduledStartTime;
            ScheduledEndTime = scheduledEndTime;
            PublishedStartTime = publishedStartTime;
            ActualStartTime = actualStartTime;
            ActualEndTime = actualEndTime;
            ChannelName = channelName;
            InternalChannelId = internalChannelId;
            Room = room;
            EventType = eventType;
            ProductionSource = productionSource;
            LegacyMeetingId = legacyMeetingId;
            ExternalLocation = externalLocation;
            SubTitle = subTitle;
            HomeFilters = new HomeFilter(States);
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string House { get; private set; }
        public string Business { get; private set; }
        public EventStates States { get; private set; }
        public DateTime DisplayStartDate { get; private set; }
        public DateTime DisplayEndDate { get; private set; }
        public DateTime? ActualLiveStartTime { get; private set; }
        public DateTime ScheduledStartTime { get; private set; }
        public DateTime ScheduledEndTime { get; private set; }
        public DateTime? PublishedStartTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }
        public DateTime? ActualEndTime { get; private set; }
        public string ChannelName { get; private set; }
        public int? InternalChannelId { get; set; }
        public string Room { get; private set; }
        public string EventType { get; private set; }
        public string ProductionSource { get; private set; }
        public int? LegacyMeetingId { get; private set; }
        public string ExternalLocation { get; private set; }
        public string SubTitle { get; private set; }
        public HomeFilter HomeFilters { get; private set; }
    }
}
