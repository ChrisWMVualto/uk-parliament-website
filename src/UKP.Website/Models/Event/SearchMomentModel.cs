using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class SearchMomentModel
    {
        public SearchMomentModel(Guid eventId, LogMomentResultModel logMomentResultModel, int skipCount)
        {
            EventId = eventId;
            LogMomentResultModel = logMomentResultModel;
            SkipCount = skipCount;
        }

        public Guid EventId { get; private set; }
        public LogMomentResultModel LogMomentResultModel { get; private set; }
        public int SkipCount { get; private set; }
    }
}