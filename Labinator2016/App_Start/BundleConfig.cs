//-----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016
{
    using System.Web.Optimization;

    /// <summary>
    /// Class to contain Bundle configurations.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class BundleConfig
    {
        /// <summary>
        /// Registers the header files in the bundle
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        /// <param name="bundles">The internal bundle collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/site.js"));
            ////bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            ////            "~/Scripts/jquery.validate*"));
            ////bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            ////            "~/Scripts/jquery-ui.js"));
            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            ////bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            ////            "~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/site.css"));
        }
    }
}
