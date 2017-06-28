using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.flip.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.easing.1.3.min.js",
                        "~/Scripts/jquery.stellar.min.js",
                        "~/Scripts/wow.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/custom.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/jquery").Include(
                        "~/Scripts/Dashboard/jquery.min.js",
                        "~/Scripts/Dashboard/bootstrap.min.js",
                        "~/Scripts/Dashboard/fastclick.js",
                        "~/Scripts/Dashboard/nprogress.js",
                        "~/Scripts/Dashboard/Chart.min.js",
                        "~/Scripts/Dashboard/gauge.min.js",
                        "~/Scripts/Dashboard/bootstrap-progressbar.min.js",
                        "~/Scripts/Dashboard/icheck.min.js",
                        "~/Scripts/Dashboard/skycons.js",
                        "~/Scripts/Dashboard/jquery.flot.js",
                        "~/Scripts/Dashboard/jquery.flot.pie.js",
                        "~/Scripts/Dashboard/jquery.flot.time.js",
                        "~/Scripts/Dashboard/jquery.flot.stack.js",
                        "~/Scripts/Dashboard/jquery.flot.resize.js",
                        "~/Scripts/Dashboard/jquery.flot.orderBars.js",
                        "~/Scripts/Dashboard/jquery.flot.spline.min.js",
                        "~/Scripts/Dashboard/curvedLines.js",
                        "~/Scripts/Dashboard/date.js",
                        "~/Scripts/Dashboard/jquery.vmap.js",
                        "~/Scripts/Dashboard/jquery.vmap.world.js",
                        "~/Scripts/Dashboard/jquert.vmap.sampledata.js",
                        "~/Scripts/Dashboard/moment.min.js",
                        "~/Scripts/Dashboard/daterangepicker.js",
                        "~/Scripts/Dashboard/custom.min.js",
                        "~/Scripts/Dashboard/dropzone.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.theme.css"));

            bundles.Add(new StyleBundle("~/Content/Dashboard/css").Include(
                      "~/Content/Dashboard/bootstrap.min.css",
                      "~/Content/Dashboard/font-awesome.min.css",
                      "~/Content/Dashboard/nprogress.css",
                      "~/Content/Dashboard/green.css",
                      "~/Content/Dashboard/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/Dashboard/jqvmap.min.css",
                      "~/Content/Dashboard/daterangepicker.css",
                      "~/Content/Dashboard/custom.css",
                      "~/Content/Dashboard/style1.css",
                      "~/Content/Dashboard/dropzone.css"));

        }
    }
}
