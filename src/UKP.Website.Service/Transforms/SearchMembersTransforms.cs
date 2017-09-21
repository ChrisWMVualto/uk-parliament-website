using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class SearchMembersTransforms
    {
        public static IEnumerable<SearchMembersNameModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if(jArray == null) return Enumerable.Empty<SearchMembersNameModel>();

            var list = new List<SearchMembersNameModel>();
            foreach(var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static SearchMembersNameModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var membersNameServiceId = (int)jObject.membersNameId;
            var member = (string)jObject.member;

            return new SearchMembersNameModel(membersNameServiceId, member);
        }
    }
}