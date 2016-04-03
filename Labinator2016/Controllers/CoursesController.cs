//-----------------------------------------------------------------------
// <copyright file="CoursesController.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// Version: 1.1 - Logging added.
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
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.Models;
    using Labinator2016.Lib.REST;
    using Labinator2016.ViewModels.DatatablesViewModel;

    /// <summary>
    /// Back-end processing for all Course-related work and pages.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class CoursesController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Handle to the Sky Tap Interface
        /// </summary>
        private ISkyTap st;

        /// <summary>
        /// Template accessor
        /// </summary>
        private Template template;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoursesController"/> class for use in testing.
        /// </summary>
        /// <param name="db">The Fake database</param>
        /// <param name="st">The Fake SkyTap</param>
        public CoursesController(ILabinatorDb db, ISkyTap st)
        {
            this.db = db;
            this.st = st;
            this.template = new Template(this.st);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoursesController"/> class for use live.
        /// </summary>
        public CoursesController()
        {
            this.db = new LabinatorContext();
            this.st = new SkyTap();
            this.template = new Template(this.st);
        }

        /// <summary>
        /// Provides a response to an AJAX request for a list of courses.
        /// </summary>
        /// <param name="param">The filter, sort and paging configuration from the DataTable</param>
        /// <returns>A JSON response with the requested data</returns>
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<Course>(this.db.Query<Course>().ToList(), param));
        }

        /// <summary>
        /// Provides a response to an AJAX request for a list of machines associated with this course.
        /// </summary>
        /// <param name="param">The filter, sort and paging configuration from the DataTable</param>
        /// <returns>A JSON response with the requested data</returns>
        public JsonResult MachineAjax(DTParameters param)
        {
            DTResult<CourseMachineTemp> result = Generic.Ajax<CourseMachineTemp>(this.db.Query<CourseMachineTemp>().Where(cmt => cmt.SessionId == param.SessionId).ToList(), param);
            return this.Json(result);
        }

        /// <summary>
        /// Called via AJAX when the Template associated with the Course is changed, in order to re-align the Temporary table.
        /// </summary>
        /// <returns>JSON status message</returns>
        public JsonResult Refresh()
        {
            string json;
            IDictionary<string, string> response = new Dictionary<string, string>();
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic parameters = serializer.Deserialize<object>(json);
            if ((parameters["Template"] != null) && (parameters["Session"] != null) && (parameters["Course"] != null))
            {
                this.PopulateTemp(int.Parse(parameters["Course"]), parameters["Template"], parameters["Session"]);
            }

            response.Add("Status", "Done");
            return this.Json(response);
        }

        /// <summary>
        /// Provides a list of all Courses currently in the system.
        /// </summary>
        /// <returns>The Index View.</returns>
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The first stage in editing a Course.
        /// </summary>
        /// <param name="id">The CourseID of the Course to edit. Zero indicates "new".</param>
        /// <returns>The edit view.</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            Template template = new Template(this.st);
            Course course = null;
            if (id == 0)
            {
                course = new Course() { CourseId = 0, Name = "New", Days = 1, Hours = 8, Template = string.Empty, StartTime = new DateTime(2015, 12, 31, 8, 30, 00) };
            }
            else
            {
                course = this.db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
                if (course == null)
                {
                    return this.HttpNotFound();
                }
            }

            ViewBag.Session = Guid.NewGuid().ToString();
            this.PopulateTemp(id, course.Template, ViewBag.Session);
            List<Template> templates = template.GetList();
            templates.Insert(0, new Template() { name = "Please Select Template...", id = string.Empty });
            ViewBag.Template = new SelectList(templates, "Id", "Name", course.Template);
            return this.View(course);
        }

        /// <summary>
        /// Writes the changes made to a Course back to the database.
        /// </summary>
        /// <param name="course">The Course object returned from the browser.</param>
        /// <param name="session">The GUID used to uniquely identify this browser session.</param>
        /// <returns>A redirection back to the list of Courses.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "courseId,Name,Days,Hours,Template,StartTime")] Course course, string session)
        {
            if (ModelState.IsValid)
            {
                if (course.CourseId == 0)
                {
                    this.db.Add<Course>(course);
                    Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message=LogMessages.create, Detail = "Course " + course.Name + " created." });
                }
                else
                {
                    this.db.Update<Course>(course);
                    Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.update, Detail = "Course " + course.Name + " updated." });
                }

                this.db.SaveChanges();
                List<CourseMachine> cms = this.db.Query<CourseMachine>().Where(cm => cm.CourseId == course.CourseId).ToList();
                foreach (CourseMachine cm in cms)
                {
                    this.db.Remove<CourseMachine>(cm);
                }

                List<CourseMachineTemp> cmts = this.db.Query<CourseMachineTemp>().Where(cm => cm.SessionId == session).ToList();
                foreach (CourseMachineTemp cmt in cmts)
                {
                    CourseMachine cm = new CourseMachine();
                    cm.CourseId = course.CourseId;
                    cm.VMId = cmt.VMId;
                    cm.IsActive = cmt.IsActive;
                    this.db.Add<CourseMachine>(cm);
                    this.db.Remove<CourseMachineTemp>(cmt);
                }

                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            ViewBag.Session = System.Web.HttpContext.Current.Session.SessionID;
            this.PopulateTemp(course.CourseId, course.Template, ViewBag.Session);
            List<Template> templates = this.template.GetList();
            templates.Insert(0, new Template() { name = "Please Select Template...", id = string.Empty });
            ViewBag.Template = new SelectList(templates, "Id", "Name", course.Template);
            return this.View(course);
        }

        /// <summary>
        /// Processes the AJAX request that activates or deactivates a machine for a course
        /// </summary>
        /// <returns>An empty response</returns>
        public JsonResult Active()
        {
            string json;
            IDictionary<string, string> response = new Dictionary<string, string>();
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic parameters = serializer.Deserialize<object>(json);
            if (parameters["configuration"] != null && parameters["active"] != null)
            {
                int courseMachineTempId = parameters["configuration"];
                CourseMachineTemp courseMachineTemp = this.db.Query<CourseMachineTemp>().Where(cmt => cmt.CourseMachineTempId == courseMachineTempId).FirstOrDefault();
                if (courseMachineTemp != null)
                {
                    courseMachineTemp.IsActive = parameters["active"];
                    this.db.Update<CourseMachineTemp>(courseMachineTemp);
                    this.db.SaveChanges();
                }
            }

            return this.Json(response);
        }

        /// <summary>
        /// Populates the temporary Course Machine table.
        /// </summary>
        /// <param name="courseId">The course identifier of the Course being edited.</param>
        /// <param name="templateId">The template identifier of the Course being edited.</param>
        /// <param name="sessionId">A unique ID to distinguish between browser sessions.</param>
        [NonAction]
        public void PopulateTemp(int courseId, string templateId, string sessionId)
        {
            Template t = new Template(this.st);
            if (templateId == string.Empty)
            {
                ////    t.VMs = new Vm[0];
            }
            else
            {
                t = t.GetTemplate(templateId);
            }

            List<CourseMachineTemp> cmts = this.db.Query<CourseMachineTemp>()
                                                    .Where(c => c.SessionId == sessionId)
                                                    .ToList();
            if (cmts != null)
            {
                foreach (CourseMachineTemp cmt in cmts)
                {
                    this.db.Remove<CourseMachineTemp>(cmt);
                }
            }

            if ((courseId == 0) || (this.db.Query<Course>()
                                            .Where(c => c.CourseId == courseId)
                                            .First()
                                            .Template != templateId))
            {
                if (t.VMs != null)
                {
                    foreach (Vm v in t.VMs)
                    {
                        CourseMachineTemp cmt = new CourseMachineTemp();
                        cmt.VMId = v.id;
                        cmt.VMName = v.name;
                        cmt.SessionId = sessionId;
                        cmt.TimeStamp = DateTime.Now;
                        cmt.IsActive = true;
                        this.db.Add<CourseMachineTemp>(cmt);
                    }
                }

                this.db.SaveChanges();
            }
            else
            {
                List<CourseMachine> cms = this.db.Query<CourseMachine>()
                                                    .Where(cm => cm.CourseId == courseId)
                                                    .ToList();
                if (cms != null)
                {
                    foreach (CourseMachine cm in cms)
                    {
                        CourseMachineTemp cmt = new CourseMachineTemp();
                        cmt.VMId = cm.VMId;
                        cmt.SessionId = sessionId;
                        cmt.TimeStamp = DateTime.Now;
                        cmt.IsActive = cm.IsActive;
                        this.db.Add<CourseMachineTemp>(cmt);
                    }

                    this.db.SaveChanges();
                }

                cmts = this.db.Query<CourseMachineTemp>()
                                .Where(c => c.SessionId == sessionId)
                                .ToList();
                foreach (Vm v in t.VMs)
                {
                    CourseMachineTemp cmt = cmts.Where(cm => cm.VMId == v.id).FirstOrDefault();
                    if (cmt == null)
                    {
                        cmt = new CourseMachineTemp();
                        cmt.VMId = v.id;
                        cmt.VMName = v.name;
                        cmt.SessionId = sessionId;
                        cmt.TimeStamp = DateTime.Now;
                        cmt.IsActive = true;
                        this.db.Add<CourseMachineTemp>(cmt);
                    }
                    else
                    {
                        cmt.VMName = v.name;
                        this.db.Update<CourseMachineTemp>(cmt);
                    }

                    this.db.SaveChanges();
                }

                foreach (CourseMachineTemp cmt in cmts)
                {
                    Vm v = t.VMs.Where(vm => vm.id == cmt.VMId).FirstOrDefault();
                    if (v == null)
                    {
                        this.db.Remove(cmt);
                    }
                }

                this.db.SaveChanges();
            }
        }

        /// <summary>
        /// Performs the first part of the two-stage deletion of a Course.
        /// </summary>
        /// <param name="id">The CourseID of the Course to delete.</param>
        /// <returns>The confirmation view</returns>
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = this.db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return this.HttpNotFound();
            }

            return this.View(course);
        }

        /// <summary>
        /// Performs the actual deletion of a Course when confirmed by the User.
        /// </summary>
        /// <param name="id">The CourseID of the Course to delete.</param>
        /// <returns>A redirection back to the list of Courses.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Course course = this.db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return this.HttpNotFound();
            }

            this.db.Remove<Course>(course);
            List<CourseMachine> cms = this.db.Query<CourseMachine>().Where(cm => cm.CourseId == id).ToList();
            foreach (CourseMachine cm in cms)
            {
                this.db.Remove<CourseMachine>(cm);
            }

            this.db.SaveChanges();
            Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.delete, Detail = "Course " + course.Name + " deleted." });

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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