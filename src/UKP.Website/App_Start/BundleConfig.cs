using System.Web.Optimization;

namespace UKP.Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/bootstrap-multiselect.css",
                "~/Content/css/autocomplete.css",
                "~/Content/css/application.css"));

            bundles.Add(new ScriptBundle("~/bundles/headJs").Include(
                "~/Content/js/vendor/modernizr-2.6.2-respond-1.1.0.min.js",
                "~/Content/js/vendor/jquery-1.10.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/footJs").Include(
                "~/Content/js/vendor/jQuery-Mobile.js",
                "~/Content/js/vendor/jquery.succinct.min.js",
                "~/Content/js/vendor/bootstrap-checkbox.js",
                "~/Content/js/vendor/jquery.pep.js",
                "~/Content/js/vendor/breakpoints.js",
                "~/Content/js/vendor/jquery.slimscroll.js",
                "~/Content/js/vendor/slick.js",
                "~/Content/js/vendor/bootstrap-datepicker.mod.js",
                "~/Content/js/vendor/bootstrap-select.js",
                "~/Content/js/vendor/bootstrap-multiselect.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/jquery.autocomplete.mod.js",
                "~/Content/js/jquery.infinitescroll.js",
                "~/Content/js/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/guideJs").Include(
                "~/Content/js/vendor/bootstrap-timepicker-guide-mod.js",
                "~/Content/js/vendor/dragscrollable.mod.js",
                "~/Content/js/guide.js"));

            bundles.Add(new ScriptBundle("~/bundles/searchJs").Include(
                "~/Content/js/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/eventJs").Include(
                "~/Scripts/jquery.signalR-2.1.1.min.js",
                "~/Content/js/jquery.post-message.updated.js",
                "~/Content/js/vendor/bootstrap-timepicker-event-mod.js",
                "~/Content/js/responsive-player.js",
                "~/Content/js/event.js",
                "~/Content/js/program-date-time.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}