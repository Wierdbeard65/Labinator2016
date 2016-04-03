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
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.Models;

    /// <summary>
    /// Controller class for the Home Page
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// Used for regular constructions. Obtains handle to Database.
        /// </summary>
        public HomeController()
        {
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// Used for constructing when Unit Testing.
        /// </summary>
        /// <param name="db">Handle to Database stub.</param>
        public HomeController(ILabinatorDb db)
        {
            this.db = db;
        }

        /// <summary>
        /// Initial Index
        /// </summary>
        /// <returns>Index view</returns>
        public ActionResult Index()
        {
            if (User.IsInRole("Instructor") || User.IsInRole("Administrator"))
            {
                return this.View();
            }
            else
            {
                User user = this.db.Query<User>().Where(u => u.EmailAddress == User.Identity.Name).FirstOrDefault();
                if (user == null)
                {
                    UrlHelper helper = new UrlHelper();
                    return this.Redirect("/Home/Error?Message=" + helper.Encode("User " + User.Identity.Name + " does not exist"));
                }

                DateTime start = DateTime.Now.AddHours(1);
                List<Classroom> runningClassrooms = this.db.Query<Classroom>().Include(c => c.Course).Where(c => c.Start < start).ToList();
                runningClassrooms = runningClassrooms.Where(c => c.Start.AddDays(c.Course.Days).AddHours(c.Course.Hours + 1) > DateTime.Now).ToList();
                List<int> runningClassroomIds = runningClassrooms.Select(c => c.ClassroomId).ToList();
                List<Seat> seats = this.db.Query<Seat>().Where(s => s.UserId == user.UserId && runningClassroomIds.Contains(s.ClassroomId)).ToList();
                if (seats.Count == 0)
                {
                    UrlHelper helper = new UrlHelper();
                    return this.Redirect("/Home/Error?Message=" + helper.Encode("Running Classrooms: " + runningClassrooms.Count + "<br/>"
                                                                            + "Classroom IDs :" + runningClassroomIds.ToString() + "<br/>"
                                                                            + "User Id :" + user.UserId));
                }

                return this.RedirectToAction("Connect", new { id = seats[0].SeatId });
            }
        }

        /// <summary>
        /// The Connect view is the one which displays the RDP session.
        /// </summary>
        /// <returns>Connection view</returns>
        public ActionResult Connect()
        {
            return this.View();
        }

        /// <summary>
        /// Provides the view of all the thumbnails of each <see cref="Seat"/> in the <see cref="Classroom"/> and associated control options.
        /// </summary>
        /// <param name="id">The <see cref="Classroom"/> identifier.</param>
        /// <returns>The Grid View</returns>
        public ActionResult Grid(int id)
        {
            ViewBag.ClassroomId = id;
            return this.View();
        }
    }
}