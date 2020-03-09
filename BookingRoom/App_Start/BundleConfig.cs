using System.Web;
using System.Web.Optimization;

namespace BookingRoom
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jshome").Include(
                 "~/Scripts/jquery-{version}.js",
                 "~/Content/Vendors/chart.js/vendor.bundle.base.js",
                 "~/Content/js/misc.js",
                 "~/Content/js/hoverable-collapse.js"
                 ));
        }
    }
}
