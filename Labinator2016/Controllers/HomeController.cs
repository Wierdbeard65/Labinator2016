//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Controller class for the Home Page
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Initial Index with a default view of "Owner's only"
        /// </summary>
        /// <returns>Index view</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}