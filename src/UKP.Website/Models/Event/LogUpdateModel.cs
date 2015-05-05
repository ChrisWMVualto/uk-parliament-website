using System;

namespace UKP.Website.Models.Event
{
    public class LogUpdateModel
    {
        public Guid EventId { get; set; }
        public LogUpdateType LogUpdateType { get; set; }
        public Guid LogMomentId { get; set; }
        public string Description { get; set; }
        public DateTime Timecode { get; set; }
        public string DisplayAs { get; set; }


    }

    public enum LogUpdateType
    {
        Add,
        Update,
        Delete
    }
}