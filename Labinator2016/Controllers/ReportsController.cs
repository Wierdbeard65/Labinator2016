//-----------------------------------------------------------------------
// <copyright file="ReportsController.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.Models;
    using Labinator2016.ViewModels.DatatablesViewModel;

    /// <summary>
    /// Controller for the reports view
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class ReportsController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController"/> class for use in testing.
        /// </summary>
        /// <param name="db">The Fake database</param>
        /// <param name="st">The Fake sky tap</param>
        public ReportsController(ILabinatorDb db, ISkyTap st)
        {
            this.db = db;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController"/> class for use live.
        /// </summary>
        public ReportsController()
        {
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Provides a response to an AJAX request for a list of courses.
        /// </summary>
        /// <param name="param">The filter, sort and paging configuration from the DataTable</param>
        /// <returns>A JSON response with the requested data</returns>
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<Log>(this.db.Query<Log>().ToList(), param));
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>A view to show the reports</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}