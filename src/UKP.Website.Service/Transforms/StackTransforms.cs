using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class StackTransforms
    {
        public static IEnumerable<StackModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if(jArray == null) return Enumerable.Empty<StackModel>();

            var list = new List<StackModel>();
            foreach(var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static StackModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var id = (Guid)jObject.stack.id;
            var eventId = (Guid)jObject.stack.eventId;
            var description = (string)jObject.stack.description;
            var member = (string)jObject.stack.displayAs;
            var memberRole = (string)jObject.stack.party;
            var house = (string)jObject.stack.house;
            var memeberConstituency = (string)jObject.stack.constituency;
            var memberParty = (string)jObject.stack.party;
            var sortOrder = (int?)jObject.stack.sortOrder;

            return new StackModel(id, eventId, description, member, memberRole, house, memeberConstituency, memberParty, "", "", sortOrder);
        }
    }
}