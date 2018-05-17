using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class LogMomentResultModel
    {
        public LogMomentResultModel(IEnumerable<LogMomentModel> results, int total, bool containsLogMoments, IEnumerable<SearchHighlightCollectionModel> searchHighlights)
        {
            Results = results;
            Total = total;
            ContainsLogMoments = containsLogMoments;
            SearchHighlights = searchHighlights;
        }

        public IEnumerable<LogMomentModel> Results { get; private set; }
        public int Total { get; private set; }
        public bool ContainsLogMoments { get; private set; }
        public IEnumerable<SearchHighlightCollectionModel> SearchHighlights { get; private set; }
    }
}
