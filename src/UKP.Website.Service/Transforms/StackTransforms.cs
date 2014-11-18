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
            var title = (string)jObject.stack.title;
            var member = (string)jObject.stack.member;
            var memberRole = (string)jObject.stack.memberRole;
            var memberContext = (string)jObject.stack.memberContext;
            var noneMember = (string)jObject.stack.noneMember;
            var noneMemberContext = (string)jObject.stack.noneMemberContext;
            var sortOrder = (int?)jObject.stack.sortOrder;

            return new StackModel(id, eventId, title, member, memberRole, memberContext, noneMember, noneMemberContext, sortOrder);
        }
    }
}