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
    public partial class EventController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EventController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult GetShareVideo()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetShareVideo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetMainVideo()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetMainVideo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult EventTitle()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.EventTitle);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.PartialViewResult Clipping()
        {
            return new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Clipping);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult LegacyPageRoute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LegacyPageRoute);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult State()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.State);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EventController Actions { get { return MVC.Event; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Event";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Event";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetShareVideo = "GetShareVideo";
            public readonly string GetMainVideo = "GetMainVideo";
            public readonly string EventTitle = "EventTitle";
            public readonly string Clipping = "Clipping";
            public readonly string LegacyPageRoute = "LegacyPageRoute";
            public readonly string State = "State";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetShareVideo = "GetShareVideo";
            public const string GetMainVideo = "GetMainVideo";
            public const string EventTitle = "EventTitle";
            public const string Clipping = "Clipping";
            public const string LegacyPageRoute = "LegacyPageRoute";
            public const string State = "State";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string id = "id";
            public readonly string @in = "in";
            public readonly string @out = "out";
            public readonly string audioOnly = "audioOnly";
            public readonly string autoStart = "autoStart";
        }
        static readonly ActionParamsClass_GetShareVideo s_params_GetShareVideo = new ActionParamsClass_GetShareVideo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetShareVideo GetShareVideoParams { get { return s_params_GetShareVideo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetShareVideo
        {
            public readonly string id = "id";
            public readonly string @in = "in";
            public readonly string @out = "out";
        }
        static readonly ActionParamsClass_GetMainVideo s_params_GetMainVideo = new ActionParamsClass_GetMainVideo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetMainVideo GetMainVideoParams { get { return s_params_GetMainVideo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetMainVideo
        {
            public readonly string id = "id";
            public readonly string @in = "in";
            public readonly string @out = "out";
            public readonly string audioOnly = "audioOnly";
        }
        static readonly ActionParamsClass_EventTitle s_params_EventTitle = new ActionParamsClass_EventTitle();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EventTitle EventTitleParams { get { return s_params_EventTitle; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EventTitle
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Clipping s_params_Clipping = new ActionParamsClass_Clipping();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Clipping ClippingParams { get { return s_params_Clipping; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Clipping
        {
            public readonly string id = "id";
            public readonly string @in = "in";
            public readonly string @out = "out";
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
        static readonly ActionParamsClass_State s_params_State = new ActionParamsClass_State();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_State StateParams { get { return s_params_State; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_State
        {
            public readonly string stateChangeModel = "stateChangeModel";
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
                public readonly string _Clipping = "_Clipping";
                public readonly string _EventTitle = "_EventTitle";
                public readonly string _Info = "_Info";
                public readonly string _Share = "_Share";
                public readonly string _Stack = "_Stack";
                public readonly string Index = "Index";
            }
            public readonly string _Clipping = "~/Views/Event/_Clipping.cshtml";
            public readonly string _EventTitle = "~/Views/Event/_EventTitle.cshtml";
            public readonly string _Info = "~/Views/Event/_Info.cshtml";
            public readonly string _Share = "~/Views/Event/_Share.cshtml";
            public readonly string _Stack = "~/Views/Event/_Stack.cshtml";
            public readonly string Index = "~/Views/Event/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EventController : UKP.Website.Controllers.EventController
    {
        public T4MVC_EventController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, string @in, string @out, bool? audioOnly, bool? autoStart);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(System.Guid id, string @in, string @out, bool? audioOnly, bool? autoStart)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "in", @in);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "out", @out);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "audioOnly", audioOnly);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "autoStart", autoStart);
            IndexOverride(callInfo, id, @in, @out, audioOnly, autoStart);
            return callInfo;
        }

        [NonAction]
        partial void GetShareVideoOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Guid id, System.TimeSpan? @in, System.TimeSpan? @out);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetShareVideo(System.Guid id, System.TimeSpan? @in, System.TimeSpan? @out)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetShareVideo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "in", @in);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "out", @out);
            GetShareVideoOverride(callInfo, id, @in, @out);
            return callInfo;
        }

        [NonAction]
        partial void GetMainVideoOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Guid id, string @in, string @out, bool? audioOnly);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetMainVideo(System.Guid id, string @in, string @out, bool? audioOnly)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetMainVideo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "in", @in);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "out", @out);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "audioOnly", audioOnly);
            GetMainVideoOverride(callInfo, id, @in, @out, audioOnly);
            return callInfo;
        }

        [NonAction]
        partial void EventTitleOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult EventTitle(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.EventTitle);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EventTitleOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ClippingOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, System.Guid id, string @in, string @out);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult Clipping(System.Guid id, string @in, string @out)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Clipping);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "in", @in);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "out", @out);
            ClippingOverride(callInfo, id, @in, @out);
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

        [NonAction]
        partial void StateOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, UKP.Website.Models.Event.StateChangeModel stateChangeModel);

        [NonAction]
        public override System.Web.Mvc.HttpStatusCodeResult State(UKP.Website.Models.Event.StateChangeModel stateChangeModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.State);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stateChangeModel", stateChangeModel);
            StateOverride(callInfo, stateChangeModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
