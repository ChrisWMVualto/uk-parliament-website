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
                    "~/Content/css/theme.css",
                    "~/Content/css/autocomplete.css",
                    "~/Content/css/application.css"));
        }
    }
}