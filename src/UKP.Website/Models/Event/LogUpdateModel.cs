using System;

namespace UKP.Website.Models.Event
{
    public class LogUpdateModel
    {
        public Guid EventId { get; set; }
        public LogUpdateType LogUpdateType { get; set; }
        public Guid LogMomentId { get; set; }
        public string Title { get; set; }
        public DateTime Timecode { get; set; }
        public string Member { get; set; }


    }

    public enum LogUpdateType
    {
        Add,
        Update,
        Delete
    }
}