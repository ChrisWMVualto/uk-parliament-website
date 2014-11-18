﻿// <auto-generated />
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

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC
{
    public static UKP.Website.Controllers.CookieController Cookie = new UKP.Website.Controllers.T4MVC_CookieController();
    public static UKP.Website.Controllers.EventController Event = new UKP.Website.Controllers.T4MVC_EventController();
    public static UKP.Website.Controllers.GuideController Guide = new UKP.Website.Controllers.T4MVC_GuideController();
    public static UKP.Website.Controllers.HomeController Home = new UKP.Website.Controllers.T4MVC_HomeController();
    public static UKP.Website.Controllers.NotSupportedController NotSupported = new UKP.Website.Controllers.T4MVC_NotSupportedController();
    public static UKP.Website.Controllers.PlayerController Player = new UKP.Website.Controllers.T4MVC_PlayerController();
    public static UKP.Website.Controllers.SearchController Search = new UKP.Website.Controllers.T4MVC_SearchController();
    public static T4MVC.SearchApiController SearchApi = new T4MVC.SearchApiController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_PartialViewResult : System.Web.Mvc.PartialViewResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_PartialViewResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_HttpStatusCodeResult : System.Web.Mvc.HttpStatusCodeResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_HttpStatusCodeResult(string area, string controller, string action, string protocol = null): base(default(System.Net.HttpStatusCode))
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string jquery_2_1_1_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.1.intellisense.min.js") ? Url("jquery-2.1.1.intellisense.min.js") : Url("jquery-2.1.1.intellisense.js");
        public static readonly string jquery_2_1_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.1.min.js") ? Url("jquery-2.1.1.min.js") : Url("jquery-2.1.1.js");
        public static readonly string jquery_2_1_1_min_js = Url("jquery-2.1.1.min.js");
        public static readonly string jquery_2_1_1_min_map = Url("jquery-2.1.1.min.map");
        public static readonly string jquery_signalR_2_1_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.signalR-2.1.1.min.js") ? Url("jquery.signalR-2.1.1.min.js") : Url("jquery.signalR-2.1.1.js");
        public static readonly string jquery_signalR_2_1_1_min_js = Url("jquery.signalR-2.1.1.min.js");
        public static readonly string modernizr_2_8_3_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/modernizr-2.8.3.min.js") ? Url("modernizr-2.8.3.min.js") : Url("modernizr-2.8.3.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class css {
            private const string URLPATH = "~/Content/css";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string ajax_loader_gif = Url("ajax-loader.gif");
            public static readonly string application_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/application.min.css") ? Url("application.min.css") : Url("application.css");
                 
            public static readonly string autocomplete_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/autocomplete.min.css") ? Url("autocomplete.min.css") : Url("autocomplete.css");
                 
            public static readonly string bootstrap_multiselect_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-multiselect.min.css") ? Url("bootstrap-multiselect.min.css") : Url("bootstrap-multiselect.css");
                 
            public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
                 
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class fonts {
                private const string URLPATH = "~/Content/css/fonts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string slick_eot = Url("slick.eot");
                public static readonly string slick_svg = Url("slick.svg");
                public static readonly string slick_ttf = Url("slick.ttf");
                public static readonly string slick_woff = Url("slick.woff");
            }
        
            public static readonly string theme_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/theme.min.css") ? Url("theme.min.css") : Url("theme.css");
                 
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class fonts {
            private const string URLPATH = "~/Content/fonts";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class font_app {
                private const string URLPATH = "~/Content/fonts/font-app";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class fonts {
                    private const string URLPATH = "~/Content/fonts/font-app/fonts";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string app_eot = Url("app.eot");
                    public static readonly string app_svg = Url("app.svg");
                    public static readonly string app_ttf = Url("app.ttf");
                    public static readonly string app_woff = Url("app.woff");
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class less {
                    private const string URLPATH = "~/Content/fonts/font-app/less";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string font_app_less = Url("font-app.less");
                }
            
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class font_awesome {
                private const string URLPATH = "~/Content/fonts/font-awesome";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class css {
                    private const string URLPATH = "~/Content/fonts/font-awesome/css";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string font_awesome_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/font-awesome.min.css") ? Url("font-awesome.min.css") : Url("font-awesome.css");
                         
                    public static readonly string font_awesome_min_css = Url("font-awesome.min.css");
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class fonts {
                    private const string URLPATH = "~/Content/fonts/font-awesome/fonts";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string fontawesome_webfont_eot = Url("fontawesome-webfont.eot");
                    public static readonly string fontawesome_webfont_svg = Url("fontawesome-webfont.svg");
                    public static readonly string fontawesome_webfont_ttf = Url("fontawesome-webfont.ttf");
                    public static readonly string fontawesome_webfont_woff = Url("fontawesome-webfont.woff");
                    public static readonly string FontAwesome_otf = Url("FontAwesome.otf");
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class less {
                    private const string URLPATH = "~/Content/fonts/font-awesome/less";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string bordered_pulled_less = Url("bordered-pulled.less");
                    public static readonly string core_less = Url("core.less");
                    public static readonly string fixed_width_less = Url("fixed-width.less");
                    public static readonly string font_awesome_less = Url("font-awesome.less");
                    public static readonly string icons_less = Url("icons.less");
                    public static readonly string larger_less = Url("larger.less");
                    public static readonly string list_less = Url("list.less");
                    public static readonly string mixins_less = Url("mixins.less");
                    public static readonly string path_less = Url("path.less");
                    public static readonly string rotated_flipped_less = Url("rotated-flipped.less");
                    public static readonly string spinning_less = Url("spinning.less");
                    public static readonly string stacked_less = Url("stacked.less");
                    public static readonly string variables_less = Url("variables.less");
                }
            
            }
        
            public static readonly string glyphicons_halflings_regular_eot = Url("glyphicons-halflings-regular.eot");
            public static readonly string glyphicons_halflings_regular_svg = Url("glyphicons-halflings-regular.svg");
            public static readonly string glyphicons_halflings_regular_ttf = Url("glyphicons-halflings-regular.ttf");
            public static readonly string glyphicons_halflings_regular_woff = Url("glyphicons-halflings-regular.woff");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class karla {
                private const string URLPATH = "~/Content/fonts/karla";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class css {
                    private const string URLPATH = "~/Content/fonts/karla/css";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class fonts {
                    private const string URLPATH = "~/Content/fonts/karla/fonts";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string karla_bold_webfont_eot = Url("karla-bold-webfont.eot");
                    public static readonly string karla_bold_webfont_svg = Url("karla-bold-webfont.svg");
                    public static readonly string karla_bold_webfont_ttf = Url("karla-bold-webfont.ttf");
                    public static readonly string karla_bold_webfont_woff = Url("karla-bold-webfont.woff");
                    public static readonly string karla_regular_webfont_eot = Url("karla-regular-webfont.eot");
                    public static readonly string karla_regular_webfont_svg = Url("karla-regular-webfont.svg");
                    public static readonly string karla_regular_webfont_ttf = Url("karla-regular-webfont.ttf");
                    public static readonly string karla_regular_webfont_woff = Url("karla-regular-webfont.woff");
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class less {
                    private const string URLPATH = "~/Content/fonts/karla/less";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string karla_bold_less = Url("karla_bold.less");
                    public static readonly string karla_regular_less = Url("karla_regular.less");
                }
            
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class img {
            private const string URLPATH = "~/Content/img";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string arrow_scroll_left_png = Url("arrow-scroll-left.png");
            public static readonly string arrow_scroll_right_png = Url("arrow-scroll-right.png");
            public static readonly string blank_png = Url("blank.png");
            public static readonly string channel_five_logo_png = Url("channel-five-logo.png");
            public static readonly string channel_four_logo_png = Url("channel-four-logo.png");
            public static readonly string channel_one_logo_png = Url("channel-one-logo.png");
            public static readonly string channel_three_logo_png = Url("channel-three-logo.png");
            public static readonly string channel_two_logo_png = Url("channel-two-logo.png");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class favicons {
                private const string URLPATH = "~/Content/img/favicons";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string apple_touch_icon_114x114_precomposed_png = Url("apple-touch-icon-114x114-precomposed.png");
                public static readonly string apple_touch_icon_144x144_precomposed_png = Url("apple-touch-icon-144x144-precomposed.png");
                public static readonly string apple_touch_icon_57x57_precomposed_png = Url("apple-touch-icon-57x57-precomposed.png");
                public static readonly string apple_touch_icon_72x72_precomposed_png = Url("apple-touch-icon-72x72-precomposed.png");
                public static readonly string apple_touch_icon_precomposed_png = Url("apple-touch-icon-precomposed.png");
                public static readonly string favicon_ico = Url("favicon.ico");
                public static readonly string favicon_png = Url("favicon.png");
                public static readonly string metro_tileicon_png = Url("metro-tileicon.png");
            }
        
            public static readonly string filler_player_image_jpg = Url("filler-player-image.jpg");
            public static readonly string filler_thumb_1_jpg = Url("filler-thumb-1.jpg");
            public static readonly string filler_thumb_2_jpg = Url("filler-thumb-2.jpg");
            public static readonly string filler_thumb_3_jpg = Url("filler-thumb-3.jpg");
            public static readonly string filler_thumb_4_jpg = Url("filler-thumb-4.jpg");
            public static readonly string icons_png = Url("icons.png");
            public static readonly string logo_main_png = Url("logo-main.png");
            public static readonly string rotate_png = Url("rotate.png");
            public static readonly string shadow_right_png = Url("shadow-right.png");
            public static readonly string share_jpg = Url("share.jpg");
            public static readonly string theme_committees_blurred_jpg = Url("theme-committees-blurred.jpg");
            public static readonly string theme_committees_normal_jpg = Url("theme-committees-normal.jpg");
            public static readonly string theme_commons_blurred_jpg = Url("theme-commons-blurred.jpg");
            public static readonly string theme_commons_normal_jpg = Url("theme-commons-normal.jpg");
            public static readonly string theme_lords_blurred_jpg = Url("theme-lords-blurred.jpg");
            public static readonly string theme_lords_normal_jpg = Url("theme-lords-normal.jpg");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class js {
            private const string URLPATH = "~/Content/js";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
            public static readonly string event_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/event.min.js") ? Url("event.min.js") : Url("event.js");
            public static readonly string guide_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/guide.min.js") ? Url("guide.min.js") : Url("guide.js");
            public static readonly string jquery_autocomplete_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.autocomplete.min.js") ? Url("jquery.autocomplete.min.js") : Url("jquery.autocomplete.js");
            public static readonly string jquery_infinitescroll_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.infinitescroll.min.js") ? Url("jquery.infinitescroll.min.js") : Url("jquery.infinitescroll.js");
            public static readonly string jquery_post_message_updated_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.post-message.updated.min.js") ? Url("jquery.post-message.updated.min.js") : Url("jquery.post-message.updated.js");
            public static readonly string log_moments_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/log-moments.min.js") ? Url("log-moments.min.js") : Url("log-moments.js");
            public static readonly string responsive_player_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/responsive-player.min.js") ? Url("responsive-player.min.js") : Url("responsive-player.js");
            public static readonly string scripts_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/scripts.min.js") ? Url("scripts.min.js") : Url("scripts.js");
            public static readonly string search_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/search.min.js") ? Url("search.min.js") : Url("search.js");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class vendor {
                private const string URLPATH = "~/Content/js/vendor";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string bootstrap_checkbox_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-checkbox.min.js") ? Url("bootstrap-checkbox.min.js") : Url("bootstrap-checkbox.js");
                public static readonly string bootstrap_datepicker_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-datepicker.min.js") ? Url("bootstrap-datepicker.min.js") : Url("bootstrap-datepicker.js");
                public static readonly string bootstrap_multiselect_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-multiselect.min.js") ? Url("bootstrap-multiselect.min.js") : Url("bootstrap-multiselect.js");
                public static readonly string bootstrap_select_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-select.min.js") ? Url("bootstrap-select.min.js") : Url("bootstrap-select.js");
                public static readonly string bootstrap_timepicker_mod_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-timepicker-mod.min.js") ? Url("bootstrap-timepicker-mod.min.js") : Url("bootstrap-timepicker-mod.js");
                public static readonly string breakpoints_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/breakpoints.min.js") ? Url("breakpoints.min.js") : Url("breakpoints.js");
                public static readonly string dragscrollable_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/dragscrollable.min.js") ? Url("dragscrollable.min.js") : Url("dragscrollable.js");
                public static readonly string jquery_1_10_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.10.1.min.js") ? Url("jquery-1.10.1.min.js") : Url("jquery-1.10.1.js");
                public static readonly string jquery_1_10_1_min_js = Url("jquery-1.10.1.min.js");
                public static readonly string jQuery_Mobile_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jQuery-Mobile.min.js") ? Url("jQuery-Mobile.min.js") : Url("jQuery-Mobile.js");
                public static readonly string jquery_pep_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.pep.min.js") ? Url("jquery.pep.min.js") : Url("jquery.pep.js");
                public static readonly string jquery_slimscroll_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.slimscroll.min.js") ? Url("jquery.slimscroll.min.js") : Url("jquery.slimscroll.js");
                public static readonly string jquery_succinct_min_js = Url("jquery.succinct.min.js");
                public static readonly string modernizr_2_6_2_respond_1_1_0_min_js = Url("modernizr-2.6.2-respond-1.1.0.min.js");
                public static readonly string slick_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/slick.min.js") ? Url("slick.min.js") : Url("slick.js");
                public static readonly string stickyfloat_min_js = Url("stickyfloat.min.js");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class less {
            private const string URLPATH = "~/Content/less";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string alerts_less = Url("alerts.less");
            public static readonly string badges_less = Url("badges.less");
            public static readonly string bootstrap_less = Url("bootstrap.less");
            public static readonly string breadcrumbs_less = Url("breadcrumbs.less");
            public static readonly string button_groups_less = Url("button-groups.less");
            public static readonly string buttons_less = Url("buttons.less");
            public static readonly string carousel_less = Url("carousel.less");
            public static readonly string close_less = Url("close.less");
            public static readonly string code_less = Url("code.less");
            public static readonly string component_animations_less = Url("component-animations.less");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class custom {
                private const string URLPATH = "~/Content/less/custom";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string bootstrap_checkbox_less = Url("bootstrap-checkbox.less");
                public static readonly string bootstrap_select_variables_less = Url("bootstrap-select-variables.less");
                public static readonly string bootstrap_select_less = Url("bootstrap-select.less");
                public static readonly string custom_variables_less = Url("custom-variables.less");
                public static readonly string datepicker3_less = Url("datepicker3.less");
                public static readonly string slick_less = Url("slick.less");
                public static readonly string timepicker_less = Url("timepicker.less");
            }
        
            public static readonly string dropdowns_less = Url("dropdowns.less");
            public static readonly string forms_less = Url("forms.less");
            public static readonly string glyphicons_less = Url("glyphicons.less");
            public static readonly string grid_less = Url("grid.less");
            public static readonly string input_groups_less = Url("input-groups.less");
            public static readonly string jumbotron_less = Url("jumbotron.less");
            public static readonly string labels_less = Url("labels.less");
            public static readonly string list_group_less = Url("list-group.less");
            public static readonly string media_less = Url("media.less");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class mixins {
                private const string URLPATH = "~/Content/less/mixins";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string alerts_less = Url("alerts.less");
                public static readonly string background_variant_less = Url("background-variant.less");
                public static readonly string border_radius_less = Url("border-radius.less");
                public static readonly string buttons_less = Url("buttons.less");
                public static readonly string center_block_less = Url("center-block.less");
                public static readonly string clearfix_less = Url("clearfix.less");
                public static readonly string forms_less = Url("forms.less");
                public static readonly string gradients_less = Url("gradients.less");
                public static readonly string grid_framework_less = Url("grid-framework.less");
                public static readonly string grid_less = Url("grid.less");
                public static readonly string hide_text_less = Url("hide-text.less");
                public static readonly string image_less = Url("image.less");
                public static readonly string labels_less = Url("labels.less");
                public static readonly string list_group_less = Url("list-group.less");
                public static readonly string nav_divider_less = Url("nav-divider.less");
                public static readonly string nav_vertical_align_less = Url("nav-vertical-align.less");
                public static readonly string opacity_less = Url("opacity.less");
                public static readonly string pagination_less = Url("pagination.less");
                public static readonly string panels_less = Url("panels.less");
                public static readonly string progress_bar_less = Url("progress-bar.less");
                public static readonly string reset_filter_less = Url("reset-filter.less");
                public static readonly string resize_less = Url("resize.less");
                public static readonly string responsive_visibility_less = Url("responsive-visibility.less");
                public static readonly string size_less = Url("size.less");
                public static readonly string tab_focus_less = Url("tab-focus.less");
                public static readonly string table_row_less = Url("table-row.less");
                public static readonly string text_emphasis_less = Url("text-emphasis.less");
                public static readonly string text_overflow_less = Url("text-overflow.less");
                public static readonly string vendor_prefixes_less = Url("vendor-prefixes.less");
            }
        
            public static readonly string mixins_less = Url("mixins.less");
            public static readonly string modals_less = Url("modals.less");
            public static readonly string navbar_less = Url("navbar.less");
            public static readonly string navs_less = Url("navs.less");
            public static readonly string normalize_less = Url("normalize.less");
            public static readonly string pager_less = Url("pager.less");
            public static readonly string pagination_less = Url("pagination.less");
            public static readonly string panels_less = Url("panels.less");
            public static readonly string popovers_less = Url("popovers.less");
            public static readonly string print_less = Url("print.less");
            public static readonly string progress_bars_less = Url("progress-bars.less");
            public static readonly string responsive_embed_less = Url("responsive-embed.less");
            public static readonly string responsive_utilities_less = Url("responsive-utilities.less");
            public static readonly string scaffolding_less = Url("scaffolding.less");
            public static readonly string tables_less = Url("tables.less");
            public static readonly string theme_less = Url("theme.less");
            public static readonly string thumbnails_less = Url("thumbnails.less");
            public static readonly string tooltip_less = Url("tooltip.less");
            public static readonly string type_less = Url("type.less");
            public static readonly string utilities_less = Url("utilities.less");
            public static readonly string variables_less = Url("variables.less");
            public static readonly string wells_less = Url("wells.less");
        }
    
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009


