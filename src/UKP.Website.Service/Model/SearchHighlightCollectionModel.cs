using System;
using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class SearchHighlightCollectionModel
    {
        public SearchHighlightCollectionModel(Guid id, IEnumerable<SearchHighlightFieldModel> fields, IEnumerable<string> keywordMatches)
        {
            Id = id;
            Fields = fields;
            KeywordMatches = keywordMatches;
        }

        public Guid Id { get; private set; }
        public IEnumerable<SearchHighlightFieldModel> Fields { get; private set; }
        public IEnumerable<string> KeywordMatches { get; private set; }
    }
}