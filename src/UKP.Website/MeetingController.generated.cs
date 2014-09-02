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
    public partial class MeetingController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected MeetingController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult LegacyPageRoute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LegacyPageRoute);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MeetingController Actions { get { return MVC.Meeting; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Meeting";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Meeting";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string LegacyPageRoute = "LegacyPageRoute";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string LegacyPageRoute = "LegacyPageRoute";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string id = "id";
            public readonly string inPoint = "inPoint";
            public readonly string outPoint = "outPoint";
        }
        static readonly ActionParamsClass_LegacyPageRoute s_params_LegacyPageRoute = new ActionParamsClass_LegacyPageRoute();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_LegacyPageRoute LegacyPageRouteParams { get { return s_params_LegacyPageRoute; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_LegacyPageRoute
        {
            public readonly string meetingId = "meetingId";
            public readonly string st = "st";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MeetingController : UKP.Website.Controllers.MeetingController
    {
        public T4MVC_MeetingController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, System.DateTime? inPoint, System.DateTime? outPoint);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(System.Guid id, System.DateTime? inPoint, System.DateTime? outPoint)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "inPoint", inPoint);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "outPoint", outPoint);
            IndexOverride(callInfo, id, inPoint, outPoint);
            return callInfo;
        }

        [NonAction]
        partial void LegacyPageRouteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int meetingId, System.TimeSpan? st);

        [NonAction]
        public override System.Web.Mvc.ActionResult LegacyPageRoute(int meetingId, System.TimeSpan? st)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LegacyPageRoute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "meetingId", meetingId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "st", st);
            LegacyPageRouteOverride(callInfo, meetingId, st);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
