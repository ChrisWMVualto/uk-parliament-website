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
            var description = (string)jObject.stack.description;
            var iasDisplayAs = (string)jObject.stack.iasDisplayAs;
            var sortOrder = (int?)jObject.stack.sortOrder;

            return new StackModel(id, description, iasDisplayAs, sortOrder);
        }
    }
}