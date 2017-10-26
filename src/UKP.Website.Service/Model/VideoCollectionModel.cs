using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class VideoCollectionModel
    {
        public VideoCollectionModel(IEnumerable<VideoModel> results, int totalCount, int pageSize, IEnumerable<SearchHighlightCollectionModel> searchHighlights)
        {
            Results = results;
            TotalCount = totalCount;
            PageSize = pageSize;
            SearchHighlights = searchHighlights;
        }

        public IEnumerable<VideoModel> Results { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<SearchHighlightCollectionModel> SearchHighlights { get; private set; }
    }
}
