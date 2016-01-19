//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// Version: 1.1 - Use of Viewmodel added to Index().
/// Version: 1.2 - Support for postback of Index() added.
/// </summary>
namespace Labinator2016.Controllers
{
    using System.Web.Mvc;
    using ViewModels;

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
            IndexViewModel model = new IndexViewModel() { ShowAll = false };
            return this.View(model);
        }

        /// <summary>
        /// Index if re-display is required (e.g. if the scope of classes is changed).
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexViewModel model)
        {
            return this.View(model);
        }
    }
}