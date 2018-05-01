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

        public static bool CookieStateSet(this HttpRequestBase request)
        {
            var cookie = request.Cookies.Get(ApplicationConstants.AcceptCookieName);
            return cookie != null;
        }

        public static bool CookiesAllowed(this HttpRequestBase request)
        {
            var cookie = request.Cookies.Get(ApplicationConstants.AcceptCookieName);

            if (cookie != null)
                return cookie.Value.ToLower() == "true";

            return false;
        }

        public static MvcHtmlString FormatText(this HtmlHelper helper, string text)
        {
            if(text == null) return null;
            text = text.Replace("\\r\\n\n", Environment.NewLine);
            return new MvcHtmlString(text.Replace(Environment.NewLine, "</br>"));
        }

        public static MvcHtmlString TrimText(this HtmlHelper helper, string input, int wordCount, string elipsesUrl)
        {
            var allWords = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var trimmed = string.Join(" ", allWords.Take(wordCount));
            if(allWords.Count > wordCount) trimmed += string.Format("<a href='{0}'>....<strong>more</strong></a>", elipsesUrl);  
            return new MvcHtmlString(trimmed);
        }

        public static string IPAddress(this HttpRequestBase request)
        {
            var userIp = request.UserHostAddress;
            var forwardedHeaderIP = request.Headers["X-Forwarded-For"];
            if(forwardedHeaderIP != null) userIp = forwardedHeaderIP; // overwrite if load balancer IP exists 
            return userIp;
        }

 
    }
}