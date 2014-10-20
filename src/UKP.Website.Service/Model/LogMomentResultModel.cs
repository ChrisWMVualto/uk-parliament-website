using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class LogMomentResultModel
    {
        public IEnumerable<LogMomentModel> Results { get; private set; }
        public int Total { get; private set; }

        public LogMomentResultModel(IEnumerable<LogMomentModel> results, int total)
        {
            Results = results;
            Total = total;
        }
    }
}
