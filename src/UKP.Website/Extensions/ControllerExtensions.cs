using System.Web.Mvc;

namespace UKP.Website.Extensions
{
    public static class ControllerExtensions
    {
        public static JsonResult JsonFormatted(this Controller controller, object data, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
                   {
                       Data = data,
                       JsonRequestBehavior = behavior
                   };
        }
    }
}