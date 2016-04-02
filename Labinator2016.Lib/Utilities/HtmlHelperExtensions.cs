//-----------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// </summary>
namespace Labinator2016.Lib.Utilities
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Sundry extensions to shove stuff into pages....
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Get the Current Version from the AssemblyInfo.cs file.
        /// </summary>
        /// <param name="helper">The helper.?!</param>
        /// <returns>The current version</returns>
        public static MvcHtmlString CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                string response;
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                response = version.ToString();
                Assembly assembly = Assembly.GetExecutingAssembly();
                object[] attributes = assembly.GetCustomAttributes(true);
                var config = attributes.OfType<AssemblyDescriptionAttribute>().FirstOrDefault();
                if (config != null)
                {
                    response = response + " " + config.Description;
                }

                return MvcHtmlString.Create(response);
            }
            catch
            {
                return MvcHtmlString.Create("?.?.?.?");
            }
        }
    }
}