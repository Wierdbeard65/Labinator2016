namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Lib.Headers;
    using Lib.Models;
    using ViewModels.DatatablesViewModel;
    public class ClassroomsController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for regular constructions. Obtains handle to Database.
        /// </summary>
        public ClassroomsController()
        {
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassroomsController"/> class.
        /// Used for constructing when Unit Testing.
        /// </summary>
        /// <param name="db">Handle to Database stub.</param>
        public ClassroomsController(ILabinatorDb db)
        {
            this.db = db;
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

            ViewBag.DataCenterId = new SelectList(this.db.Query<DataCenter>(), "DataCenterId", "Name", classroom.DataCenterId);
            ViewBag.CourseId = new SelectList(this.db.Query<Course>(), "CourseId", "Name", classroom.CourseId);
            ViewBag.UserId = new SelectList(this.db.Query<User>(), "UserId", "EmailAddress", classroom.UserId);
            return this.View(classroom);
        }

        /// <summary>
        /// Writes the changes made to a Classroom back to the database.
        /// </summary>
        /// <param name="course">The Classroom object returned from the browser.</param>
        /// <returns>A redirection back to the list of Classroom.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassroomId,CourseId,DataCenterId,UserId,Start,Name")] Classroom classroom)
        {
            //// Project projectObject = new Project(_st);
            if (ModelState.IsValid)
            {
                if (classroom.ClassroomId == 0)
                {
                    ////                    String ProjectName = "Automated-" + classroom.Name + "-" + classroom.Start.ToShortDateString();
                    ////String project = projectObject.Add(ProjectName);
                    ////if (project != null)
                    ////{
                    ////    classroom.Project = project;
                    ////    //Log.Write(_db,new Log() { User = User.Identity.Name, Message = LogMessages.create, Classroom = classroom.Name });
                    this.db.Add<Classroom>(classroom);
                    Log.Write(db, new Log() {Message=LogMessages.create,Detail="Classroom created for "+classroom.course+" on "+classroom.jsDate });
                    ////}
                }

                else
                {
                    this.db.Update<Classroom>(classroom);
                    Log.Write(db,new Log() {Message=LogMessages.update, Detail= "Classroom updated for  " + classroom.course + " on " + classroom.jsDate });
                }

                this.db.SaveChanges();
                return this.RedirectToAction("Index","Home");
            }

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
        /// Performs the actual deletion of a Classroom when confirmed by the user.
        /// </summary>
        /// <param name="id">The ClassroomID of the Classroom to delete.</param>
        /// <returns>A redirection back to the list of Classrooms.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classroom classroom = this.db.Query<Classroom>().Where(c => c.ClassroomId == id).FirstOrDefault();
            this.db.Remove<Classroom>(classroom);
            this.db.SaveChanges();
            Log.Write(db, new Log() {Message=LogMessages.delete, Detail = "Classroom deleted for  " + classroom.course + " on " + classroom.jsDate });
            return this.RedirectToAction("Index", "Home");
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
