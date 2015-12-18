using System;
using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface ISearchService
    {
        VideoCollectionModel Search(string keywords, int? memberId, string house, string business, DateTime? @from, DateTime? to, int pageNum, bool isMemberKeywordSearch, bool archiveOnly);
        LogMomentResultModel SearchMoments(Guid eventId, string keywords, int? memberId, int pageSize, bool isMemberKeywordSearch);
        IEnumerable<TagModel> GetTags();
        IEnumerable<SearchMembersNameModel> SearchMembers(string keywords);
    }
}
