namespace Labinator2016
{
    using System.Web.Optimization;
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery.dataTables.js",
                        "~/Scripts/site.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            //            "~/Scripts/jquery-ui.js",
            //            "~/Scripts/jquery.dataTables.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/jquery.dataTables.min.css",
                      "~/Content/site.css"));
        }
    }
}
