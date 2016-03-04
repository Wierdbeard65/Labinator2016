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
        /// <param Name="db">Handle to Database stub.</param>
        /// <param Name="st">Handle to Skytap stub.</param>
        public ClassroomsController(ILabinatorDb db, ISkyTap st)
        {
            this.db = db;
            this.st = st;
        }

        /// <summary>
        /// Used to respond to an AJAX request for a list of Classrooms. The results are then
        /// used to populate a DataTable on the Index Page.
        /// </summary>
        /// <param Name="param">The information sent from the DataTable regarding sorts, filters etc.</param>
        /// <returns>A JSON response with the new information to display.</returns>
        [AllowAnonymous]
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<Classroom>(this.db.Query<Classroom>().ToList(), param));
        }

        /// <summary>
        /// Used to respond to an AJAX request for a list of Seats. The results are then
        /// used to populate a DataTable on the Index Page.
        /// </summary>
        /// <param Name="param">The information sent from the DataTable regarding sorts, filters etc.</param>
        /// <returns>A JSON response with the new information to display.</returns>
        [AllowAnonymous]
        public ActionResult AjaxSeat(DTParameters param)
        {
            return this.Json(Generic.Ajax<SeatTemp>(this.db.Query<SeatTemp>().Where(st=>st.SessionId==param.SessionId).ToList(), param));
        }

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
        /// <param Name="id">The ClassroomID of the Course to edit. Zero indicates "new".</param>
        /// <returns>The edit view.</returns>
        public ActionResult Edit(int id)
        {
            Classroom classroom;
            if (id == 0)
            {
                classroom = new Classroom() { ClassroomId = 0 };
                User user = this.db.Query<User>().Where(u => u.EmailAddress == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if(user != null)
                {
                    classroom.UserId = user.UserId;
                }
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
        /// <param Name="course">The Classroom object returned from the browser.</param>
        /// <returns>A redirection back to the list of Classroom.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassroomId,CourseId,DataCenterId,UserId,Start,Name")] Classroom classroom, string SessionId)
        {
            if (ModelState.IsValid)
            {
                DataCenter dc = this.db.Query<DataCenter>().Where(d => d.DataCenterId == classroom.DataCenterId).FirstOrDefault();
                if (dc != null)
                {
                    Course crs = this.db.Query<Course>().Where(c => c.CourseId == classroom.CourseId).FirstOrDefault();
                    if (crs != null)
                    {
                        Project projectObject = new Project(st);
                        if (classroom.ClassroomId == 0)
                        {
                            projectObject.name = crs.Name + "-" + classroom.Start.ToShortDateString();
                            projectObject.region = dc.Region;
                            projectObject.Add();
                            if (projectObject.id != null)
                            {
                                classroom.Project = projectObject.id;
                                this.db.Add<Classroom>(classroom);
                                Log.Write(db, ControllerContext.HttpContext, new Log() { Message = LogMessages.create, Detail = "Classroom created for " + crs.Name + " on " + classroom.jsDate });
                            }

                        }
                        else
                        {
                            this.db.Update<Classroom>(classroom);
                            Log.Write(db, ControllerContext.HttpContext, new Log() { Message = LogMessages.update, Detail = "Classroom updated for  " + crs.Name + " on " + classroom.jsDate });
                        }
                        List<SeatTemp> sts = this.db.Query<SeatTemp>().Where(st => st.SessionId == SessionId).ToList();
                        List<int> stids = sts.Select(s => s.SeatId).ToList();
                        List<Seat> seatsToRemove = this.db.Query<Seat>().Where(s => !stids.Contains(s.SeatId)).ToList();
                        Configuration configuration = new Configuration(); ;
                        foreach (Seat s in seatsToRemove)
                        {
                            this.db.Remove<Seat>(s);
                            configuration.Id = s.ConfigurationId;
                            configuration.Delete();
                        }
                        foreach (SeatTemp st in sts)
                        {
                            Seat s = st.toSeat();
                            if (s.SeatId == 0)
                            {
                                if (s.ClassroomId == 0)
                                {
                                    s.ClassroomId = classroom.ClassroomId;
                                }
                                User usr = this.db.Query<User>().Where(u => u.UserId == s.UserId).FirstOrDefault();
                                configuration.Name = usr.EmailAddress + "-" + crs.Name;
                                configuration.Add(classroom.Project, classroom.course.Template, dc.GateWayBackboneId, dc.Region);
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
                return this.RedirectToAction("Index","Home");
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
        /// <param Name="id">The ClassroomID of the Classroom to delete.</param>
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
        /// Performs the actual deletion of a Classroom when confirmed by the user.
        /// </summary>
        /// <param Name="id">The ClassroomID of the Classroom to delete.</param>
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
            Project projectObject = new Project(st);
            Classroom classroom = this.db.Query<Classroom>().Where(c => c.ClassroomId == id).FirstOrDefault();
            projectObject.id = classroom.Project;
            projectObject.Delete();
            this.db.Remove<Classroom>(classroom);
            this.db.SaveChanges();
            Course crs = this.db.Query<Course>().Where(c => c.CourseId == classroom.CourseId).FirstOrDefault();
            if (crs != null)
            {
                Log.Write(db, ControllerContext.HttpContext, new Log() { Message = LogMessages.delete, Detail = "Classroom deleted for  " + crs.Name + " on " + classroom.jsDate });
            }
            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Detail()
        {
            return this.View();
        }

        /// <summary>
        /// Populates the temporary Course Machine table.
        /// </summary>
        /// <param Name="classroomId">The course identifier of the Course being edited.</param>
        /// <param Name="sessionId">A unique ID to distinguish between browser sessions.</param>
        [NonAction]
        public void PopulateTemp(int classroomId, string sessionId)
        {
            List<SeatTemp> SeatTemps = this.db.Query<SeatTemp>()
                                                    .Where(st => st.SessionId == sessionId)
                                                    .ToList();
            if (SeatTemps != null)
            {
                foreach (SeatTemp st in SeatTemps)
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
                    SeatTemp seatTemp = seat.toSeatTemp();
                    seatTemp.SessionId = sessionId;
                    seatTemp.TimeStamp = DateTime.Now;
                    this.db.Add<SeatTemp>(seatTemp);
                }
                this.db.SaveChanges();
            }
        }

        /// <summary>
        /// Processes the AJAX request that adds a seat to a classroom
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
                    string[] NewUsers = newSeats.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (NewUsers.Count() > 0)
                    {
                        foreach (string NewUser in NewUsers)
                        {
                            String NU = NewUser.ToLower();
                            User existingUser = this.db.Query<User>().Where(u => u.EmailAddress == NU).FirstOrDefault();
                            if (existingUser == null)
                            {
                                User UserToAdd = new User();
                                UserToAdd.EmailAddress = NU;
                                UserToAdd.IsInstructor = false;
                                UserToAdd.IsAdministrator = false;
                                UserToAdd.Password = PasswordHash.CreateHash("password");
                                this.db.Add<User>(UserToAdd);
                                this.db.SaveChanges();
                            }
                            existingUser = this.db.Query<User>().Where(u => u.EmailAddress == NU).FirstOrDefault();
                            SeatTemp existingSeat = this.db.Query<SeatTemp>().Where(s => (s.UserId == existingUser.UserId && s.SessionId == session)).FirstOrDefault();
                            if (existingSeat == null)
                            {
                                //                                String configurationId = Configuration.Add(project, template, NU);
                                //                                if (configurationId != null)
                                //                               {
                                SeatTemp seat = new SeatTemp() { UserId = existingUser.UserId, SessionId=session, TimeStamp = DateTime.Now, ClassroomId=int.Parse(classroom) };
                                this.db.Add<SeatTemp>(seat);
                                this.db.SaveChanges();
                                   //                                        Logit.log(new Log() { User = User.Identity.Name, Message = LogMessages.create, Seat = seat.User.EmailAddress, Classroom = classroom.Name });
//                                }
                            }
                        }
                    }
                }
        }
        response.Add("Status", "Done");
            return this.Json(response);
        }

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
