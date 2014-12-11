using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class LogMomentResultModel
    {
        public LogMomentResultModel(IEnumerable<LogMomentModel> results, int total, bool containsLogMoments)
        {
            Results = results;
            Total = total;
            ContainsLogMoments = containsLogMoments;
        }

        public IEnumerable<LogMomentModel> Results { get; private set; }
        public int Total { get; private set; }
        public bool ContainsLogMoments { get; private set; }
    }
}
