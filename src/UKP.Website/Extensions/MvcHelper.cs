using System.Collections.Generic;
using System.Linq;
using UKP.Website.Application;

namespace System.Web.Mvc.Html
{
    public static class MvcHelper
    {
        public static MvcHtmlString IsActiveRoute(this HtmlHelper helper, ActionResult action, bool matchAction = true, bool matchController = true, bool matchId = false,
                                                  string activeClassName = "active")
        {
            return IsActiveRoute(helper, new[] { action }, matchAction, matchController, matchId, activeClassName);
        }


        public static MvcHtmlString IsActiveRoute(this HtmlHelper helper, ActionResult[] actions, bool matchAction = true, bool matchController = true, bool matchId = false,
                                                  string activeClassName = "active")
        {
            var currentAction = helper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = helper.ViewContext.RouteData.GetRequiredString("controller");
            var id = helper.ViewContext.RouteData.Values["id"];

            if(helper.ViewContext.IsChildAction)
            {
                currentAction = helper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("action");
                currentController = helper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
                id = helper.ViewContext.ParentActionViewContext.RouteData.Values["id"];
            }


            foreach(var action in actions)
            {
                var result = action.GetT4MVCResult();

                object idValue;
                result.RouteValueDictionary.TryGetValue("id", out idValue);

                if(idValue == null) idValue = string.Empty;

                if(id == null) id = string.Empty;

                if(matchAction == false)
                {
                    if(result.Controller == currentController)
                    {
                        return MvcHtmlString.Create(activeClassName);
                    }

                    return MvcHtmlString.Create("");
                }

                if(matchId == false)
                {
                    if(result.Action == currentAction && result.Controller == currentController)
                    {
                        return MvcHtmlString.Create(activeClassName);
                    }
                }

                if(result.Action == currentAction
                    && result.Controller == currentController
                    && idValue.ToString().ToLower() == id.ToString().ToLower())
                {
                    return MvcHtmlString.Create(activeClassName);
                }
            }

            return MvcHtmlString.Create("");
        }

        private static readonly long DatetimeMinTimeTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
        public static double ToJavaScriptMilliSeconds(this DateTime dateTime)
        {
            var ticks = (dateTime.ToUniversalTime().Ticks - DatetimeMinTimeTicks);
            return new TimeSpan(ticks).TotalMilliseconds;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value, string defaultText, string defaultValue)
        {
            if(enumerable == null) return null;

            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();

            items.Insert(0, new SelectListItem()
            {
                Text = defaultText,
                Value = defaultValue
            });

            return items;
        }


        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value)
        {
            if(enumerable == null) return null;

            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();

            return items;
        }


        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value, string selectedValue)
        {
            if(enumerable == null) return null;

            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();

            items.First(x => x.Value == selectedValue).Selected = true;

            return items;
        }

        public static string TwitterLink(this UrlHelper url, string title)
        {
            const int maxChars = 140;
            const string twitterPrefix = "https://twitter.com/home?status=";

            var pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            var charsLeft = maxChars - pageUrl.Length - 1;
            title = title.Length < charsLeft ? title : title.Substring(0, charsLeft);
            
            var status = string.Format("{0} {1}", title, pageUrl);
            return twitterPrefix + url.Encode(status);
        }

        public static string FacebookLink(this UrlHelper url)
        {
            return "https://www.facebook.com/sharer/sharer.php?u=" + url.Encode(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public static string GooglePlusLink(this UrlHelper url)
        {
            return "https://plus.google.com/share?url=" + url.Encode(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public static string LinkedinLink(this UrlHelper url, string title, DateTime date)
        {
            var pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            var summary = string.Format("Watch the {0} from {1}", title, date.ToString(ApplicationConstants.DateFormat));

            return string.Format("https://www.linkedin.com/shareArticle?mini=true&url={0}&title={1}&summary={2}", url.Encode(pageUrl), url.Encode(title), url.Encode(summary));
        }

        public static bool CookieStateSet(this HttpRequestBase request)
        {
            return (request.Cookies.Get(ApplicationConstants.AcceptCookieName) != null || HttpContext.Current.Session[ApplicationConstants.AcceptCookieName] != null);
        }

        public static bool CookiesAllowed(this HttpRequestBase request)
        {
            var cookie = request.Cookies.Get(ApplicationConstants.AcceptCookieName);

            if (cookie != null)
                return cookie.Value.ToLower() == "true";

            return false;
        }
    }
}