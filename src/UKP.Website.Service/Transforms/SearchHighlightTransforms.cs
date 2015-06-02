using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class SearchHighlightTransforms
    {
        private static SearchHighlightCollectionModel TransformSearchHighlights(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var id = (Guid)jObject.id;

            dynamic fieldsArray = JArray.Parse(jObject.fields.ToString());
            var searchHighlightList = new List<SearchHighlightFieldModel>();
            foreach(var fieldJson in fieldsArray)
            {
                var name = (string)fieldJson.name;
                var value = (string)fieldJson.value;
                searchHighlightList.Add(new SearchHighlightFieldModel(name, value));
            }

            dynamic keywordsArray = JArray.Parse(jObject.keywordMatches.ToString());
            var keywordList = new List<string>();
            foreach(var keyword in keywordsArray)
            {
                keywordList.Add((string)keyword);
            }

            return new SearchHighlightCollectionModel(id, searchHighlightList, keywordList);
        }

        public static IEnumerable<SearchHighlightCollectionModel> TransformArray(string jsonArray)
        {
            dynamic searchHighlightsArray = JArray.Parse(jsonArray);
            if(searchHighlightsArray == null) return Enumerable.Empty<SearchHighlightCollectionModel>();

            var searchHighlightList = new List<SearchHighlightCollectionModel>();
            foreach(var json in searchHighlightsArray)
            {
                searchHighlightList.Add(TransformSearchHighlights(json));
            }

            return searchHighlightList;
        }
    }
}
