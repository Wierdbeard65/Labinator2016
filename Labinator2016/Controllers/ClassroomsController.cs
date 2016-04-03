//-----------------------------------------------------------------------
// <copyright file="ClassroomsController.cs" company="Interactive Intelligence">
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
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Lib.Headers;
    using Lib.Models;
    using Lib.REST;
    using Lib.Utilities;
    using ViewModels.DatatablesViewModel;

    /// <summary>
    /// Controller containing all of the Actions relating to the management of Classrooms.
    /// </summary>
    [Authorize]
    public class ClassroomsController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Handle to the Sky Tap interface.
        /// </summary>
        private ISkyTap st;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for regular constructions. Obtains handle to Database.
        /// </summary>
        public ClassroomsController()
        {
            this.db = new LabinatorContext();
            this.st = new SkyTap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for constructing when Unit Testing.
        /// </summary>
        /// <param name="db">Handle to Database interface stub.</param>
        /// <param name="st">Handle to Sky Tap interface stub.</param>
        public ClassroomsController(ILabinatorDb db, ISkyTap st)
        {
            this.db = db;
            this.st = st;
        }

        /// <summary>
        /// Used to respond to an AJAX request for a list of Classrooms. The results are then
        /// used to populate a DataTable on the Index Page.
        /// </summary>
        /// <param name="param">The information sent from the DataTable regarding sorts, filters etc.</param>
        /// <returns>A JSON response with the new information to display.</returns>
        [AllowAnonymous]
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<Classroom>(this.db.Query<Classroom>().ToList(), param));
        }

        /// <summary>`
        /// Used to respond to an AJAX request for a list of Seats. The results are then
        /// used to populate a DataTable on the Index Page.
        /// </summary>
        /// <param name="param">The information sent from the DataTable regarding sorts, filters etc.</param>
        /// <returns>A JSON response with the new information to display.</returns>
        [AllowAnonymous]
        public ActionResult AjaxSeat(DTParameters param)
        {
            return this.Json(Generic.Ajax<SeatTemp>(this.db.Query<SeatTemp>().Where(st => st.SessionId == param.SessionId).ToList(), param));
        }

        /// <summary>
        /// Processes the AJAX request for a list of seats associated wuth a particular classroom.
        /// </summary>
        /// <returns>A JSON representation of the Seat list.</returns>
        [AllowAnonymous]
        public JsonResult SeatGrid()
        {
            string json;
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic parameters = serializer.Deserialize<object>(json);
            List<Seat> seats = new List<Seat>();
            if (parameters["ClassroomId"] != null)
            {
                int classroomId = parameters["ClassroomId"];
                seats = this.db.Query<Seat>().Where(s => s.ClassroomId == classroomId).ToList();
            }

            return this.Json(seats);
        }

        /// <summary>
        /// The first stage in editing a Classroom.
        /// </summary>
        /// <param name="id">The ClassroomID of the Course to edit. Zero indicates "new".</param>
        /// <returns>The edit view.</returns>
        public ActionResult Edit(int id)
        {
            Classroom classroom;
            if (id == 0)
            {
                classroom = new Classroom() { ClassroomId = 0 };
                User user = this.db.Query<User>().Where(u => u.EmailAddress == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    classroom.UserId = user.UserId;
                }

                classroom.Start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 30, 0);
            }
            else
            {
                classroom = this.db.Query<Classroom>().Where(c => c.ClassroomId == id).FirstOrDefault();
                if (classroom == null)
                {
                    return this.HttpNotFound();
                }
            }

            ViewBag.Session = Guid.NewGuid().ToString();
            this.PopulateTemp(id, ViewBag.Session);
            ViewBag.DataCenterId = new SelectList(this.db.Query<DataCenter>(), "DataCenterId", "Name", classroom.DataCenterId);
            ViewBag.CourseId = new SelectList(this.db.Query<Course>(), "CourseId", "Name", classroom.CourseId);
            ViewBag.UserId = new SelectList(this.db.Query<User>(), "UserId", "EmailAddress", classroom.UserId);
            return this.View(classroom);
        }

        /// <summary>
        /// Writes the changes made to a Classroom back to the database.
        /// </summary>
        /// <param name="classroom">The Classroom object returned from the browser.</param>
        /// <param name="sessionId">The session identifier for the user's browser session</param>
        /// <returns>A redirection back to the list of Classroom.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassroomId,CourseId,DataCenterId,UserId,Start,Name")] Classroom classroom, string sessionId)
        {
            if (ModelState.IsValid)
            {
                DataCenter dc = this.db.Query<DataCenter>().Where(d => d.DataCenterId == classroom.DataCenterId).FirstOrDefault();
                if (dc != null)
                {
                    Course crs = this.db.Query<Course>().Where(c => c.CourseId == classroom.CourseId).FirstOrDefault();
                    if (crs != null)
                    {
                        Project projectObject = new Project(this.st);
                        if (classroom.ClassroomId == 0)
                        {
                            projectObject.name = crs.Name + "-" + classroom.Start.ToShortDateString();
                            projectObject.region = dc.Region;
                            projectObject.Add();
                            if (projectObject.id != null)
                            {
                                classroom.Project = projectObject.id;
                                this.db.Add<Classroom>(classroom);
                                Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.create, Detail = "Classroom created for " + crs.Name + " on " + classroom.JsDate });
                            }
                        }
                        else
                        {
                            this.db.Update<Classroom>(classroom);
                            Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.update, Detail = "Classroom updated for  " + crs.Name + " on " + classroom.JsDate });
                        }

                        List<SeatTemp> sts = this.db.Query<SeatTemp>().Where(st => st.SessionId == sessionId).ToList();
                        List<int> stids = sts.Select(s => s.SeatId).ToList();
                        List<Seat> seatsToRemove = this.db.Query<Seat>().Where(s => !stids.Contains(s.SeatId)).ToList();
                        Configuration configuration = new Configuration();
                        foreach (Seat s in seatsToRemove)
                        {
                            this.db.Remove<Seat>(s);
                            configuration.Id = s.ConfigurationId;
                            configuration.Delete();
                        }

                        foreach (SeatTemp st in sts)
                        {
                            Seat s = st.ToSeat();
                            if (s.SeatId == 0)
                            {
                                if (s.ClassroomId == 0)
                                {
                                    s.ClassroomId = classroom.ClassroomId;
                                }

                                User usr = this.db.Query<User>().Where(u => u.UserId == s.UserId).FirstOrDefault();
                                configuration.Name = usr.EmailAddress + "-" + crs.Name;
                                configuration.Add(classroom.Project, classroom.Course.Template, dc.GateWayBackboneId, dc.Region);
                                if (configuration.Id != null)
                                {
                                    s.ConfigurationId = configuration.Id;
                                    this.db.Add<Seat>(s);
                                }
                            }

                            this.db.Remove<SeatTemp>(st);
                        }

                        this.db.SaveChanges();
                    }
                }

                return this.RedirectToAction("Index", "Home");
            }

            ViewBag.Session = System.Web.HttpContext.Current.Session.SessionID;
            this.PopulateTemp(classroom.ClassroomId, ViewBag.Session);
            ViewBag.CourseId = new SelectList(this.db.Query<Course>(), "CourseId", "Name", classroom.CourseId);
            ViewBag.UserId = new SelectList(this.db.Query<User>(), "UserId", "EmailAddress", classroom.UserId);
            return this.View(classroom);
        }

        /// <summary>
        /// Performs the first part of the two-stage deletion of a Classroom.
        /// </summary>
        /// <param name="id">The ClassroomID of the Classroom to delete.</param>
        /// <returns>The confirmation view</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classroom classroom = this.db.Query<Classroom>().Where(c => c.ClassroomId == id).FirstOrDefault();
            if (classroom == null)
            {
                return this.HttpNotFound();
            }

            return this.View(classroom);
        }

        /// <summary>
        /// Performs the actual deletion of a Classroom when confirmed by the User.
        /// </summary>
        /// <param name="id">The ClassroomID of the Classroom to delete.</param>
        /// <returns>A redirection back to the list of Classrooms.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id)
        {
            List<Seat> seats = this.db.Query<Seat>()
                                                .Where(s => s.ClassroomId == id)
                                                .ToList();
            if (seats != null)
            {
                Configuration configuration = new Configuration();
                foreach (Seat seat in seats)
                {
                    configuration.Id = seat.ConfigurationId;
                    configuration.Delete();
                    this.db.Remove<Seat>(seat);
                }

                this.db.SaveChanges();
            }

            Project projectObject = new Project(this.st);
            Classroom classroom = this.db.Query<Classroom>().Where(c => c.ClassroomId == id).FirstOrDefault();
            projectObject.id = classroom.Project;
            projectObject.Delete();
            this.db.Remove<Classroom>(classroom);
            this.db.SaveChanges();
            Course crs = this.db.Query<Course>().Where(c => c.CourseId == classroom.CourseId).FirstOrDefault();
            if (crs != null)
            {
                Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.delete, Detail = "Classroom deleted for  " + crs.Name + " on " + classroom.JsDate });
            }

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Action called when the User requests the detail of a Classroom.
        /// </summary>
        /// <returns>A View containing the classroom detail</returns>
        public ActionResult Detail()
        {
            return this.View();
        }

        /// <summary>
        /// Populates the temporary Course Machine table.
        /// </summary>
        /// <param name="classroomId">The course identifier of the Course being edited.</param>
        /// <param name="sessionId">A unique ID to distinguish between browser sessions.</param>
        [NonAction]
        public void PopulateTemp(int classroomId, string sessionId)
        {
            List<SeatTemp> seatTemps = this.db.Query<SeatTemp>()
                                                    .Where(st => st.SessionId == sessionId)
                                                    .ToList();
            if (seatTemps != null)
            {
                foreach (SeatTemp st in seatTemps)
                {
                    this.db.Remove<SeatTemp>(st);
                }
            }

            List<Seat> seats = this.db.Query<Seat>()
                                                .Where(s => s.ClassroomId == classroomId)
                                                .ToList();
            if (seats != null)
            {
                foreach (Seat seat in seats)
                {
                    SeatTemp seatTemp = seat.ToSeatTemp();
                    seatTemp.SessionId = sessionId;
                    seatTemp.TimeStamp = DateTime.Now;
                    this.db.Add<SeatTemp>(seatTemp);
                }

                this.db.SaveChanges();
            }
        }

        /// <summary>
        /// Processes the AJAX request that adds a Seat to a classroom
        /// </summary>
        /// <returns>An empty response</returns>
        public JsonResult AddSeats()
        {
            string json;
            IDictionary<string, string> response = new Dictionary<string, string>();
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic parameters = serializer.Deserialize<object>(json);
            if ((parameters["NewSeats"] != null) && (parameters["Session"] != null) && (parameters["Classroom"] != null))
            {
                string newSeats = parameters["NewSeats"];
                string session = parameters["Session"];
                string classroom = parameters["Classroom"];
                if (newSeats != string.Empty)
                {
                    string[] newUsers = newSeats.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (newUsers.Count() > 0)
                    {
                        foreach (string newUser in newUsers)
                        {
                            string nu = newUser.ToLower();
                            User existingUser = this.db.Query<User>().Where(u => u.EmailAddress == nu).FirstOrDefault();
                            if (existingUser == null)
                            {
                                User userToAdd = new User();
                                userToAdd.EmailAddress = nu;
                                userToAdd.IsInstructor = false;
                                userToAdd.IsAdministrator = false;
                                userToAdd.Password = PasswordHash.CreateHash("password");
                                this.db.Add<User>(userToAdd);
                                this.db.SaveChanges();
                            }

                            existingUser = this.db.Query<User>().Where(u => u.EmailAddress == nu).FirstOrDefault();
                            SeatTemp existingSeat = this.db.Query<SeatTemp>().Where(s => (s.UserId == existingUser.UserId && s.SessionId == session)).FirstOrDefault();
                            if (existingSeat == null)
                            {
                                ////                                String configurationId = Configuration.Add(project, template, NU);
                                ////                                if (configurationId != null)
                                ////                               {
                                SeatTemp seat = new SeatTemp() { UserId = existingUser.UserId, SessionId = session, TimeStamp = DateTime.Now, ClassroomId = int.Parse(classroom) };
                                this.db.Add<SeatTemp>(seat);
                                this.db.SaveChanges();
                                ////                                   //                                        Logit.log(new Log() { User = User.Identity.Name, Message = LogMessages.create, Seat = Seat.User.EmailAddress, Classroom = classroom.Name });
                                ////                                }
                            }
                        }
                    }
                }
            }

            response.Add("Status", "Done");
            return this.Json(response);
        }

        /// <summary>
        /// When the user removes a seat from the classroom during editing, it generates an AJAX request which, in turn
        /// removes the corresponding record from the Temporary Seat table. This action processes the request.
        /// </summary>
        /// <returns>A JSON response indicating the action has been completed.</returns>
        public JsonResult RemoveSeat()
        {
            string json;
            IDictionary<string, string> response = new Dictionary<string, string>();
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic parameters = serializer.Deserialize<object>(json);
            if (parameters["SeatTempId"] != null)
            {
                int seatTempId = parameters["SeatTempId"];
                SeatTemp str = this.db.Query<SeatTemp>().Where(st => st.SeatTempId == seatTempId).FirstOrDefault();
                if (str != null)
                {
                    this.db.Remove<SeatTemp>(str);
                    this.db.SaveChanges();
                }
            }

            response.Add("Status", "Done");
            return this.Json(response);
        }

        /// <summary>
        /// Destructor for the object. Allow the disposal of the Database object before calling the base class destructor
        /// </summary>
        /// <param name="disposing">A boolean representing whether disposal is required.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
