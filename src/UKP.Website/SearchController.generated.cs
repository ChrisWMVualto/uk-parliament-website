// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace UKP.Website.Controllers
{
    public partial class SearchController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SearchController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult Moments()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Moments);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SearchController Actions { get { return MVC.Search; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Search";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Search";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Moments = "Moments";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Moments = "Moments";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
            public readonly string pageNum = "pageNum";
        }
        static readonly ActionParamsClass_Moments s_params_Moments = new ActionParamsClass_Moments();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Moments MomentsParams { get { return s_params_Moments; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Moments
        {
            public readonly string model = "model";
            public readonly string eventId = "eventId";
            public readonly string skipItems = "skipItems";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _SearchEvent = "_SearchEvent";
                public readonly string _SearchMoment = "_SearchMoment";
                public readonly string Index = "Index";
            }
            public readonly string _SearchEvent = "~/Views/Search/_SearchEvent.cshtml";
            public readonly string _SearchMoment = "~/Views/Search/_SearchMoment.cshtml";
            public readonly string Index = "~/Views/Search/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_SearchController : UKP.Website.Controllers.SearchController
    {
        public T4MVC_SearchController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, UKP.Website.Models.SearchViewModel model, int? pageNum);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(UKP.Website.Models.SearchViewModel model, int? pageNum)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pageNum", pageNum);
            IndexOverride(callInfo, model, pageNum);
            return callInfo;
        }

        [NonAction]
        partial void MomentsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, UKP.Website.Models.SearchViewModel model, string eventId, int skipItems);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult Moments(UKP.Website.Models.SearchViewModel model, string eventId, int skipItems)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Moments);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "eventId", eventId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skipItems", skipItems);
            MomentsOverride(callInfo, model, eventId, skipItems);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
