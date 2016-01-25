
namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.IO;
    using System.Net;
    using System.Web.Script.Serialization;
    using Lib.Headers;
    using Lib.Models;
    using ViewModels.DatatablesViewModel;
    using Labinator2016.Lib.Models;
    using Lib.REST;
    using Labinator.Lib.REST;
    public class CoursesController : Controller
    {
        ILabinatorDb db;
        ISkyTap st;
        Template template;

        public CoursesController(ILabinatorDb db, ISkyTap st)
        {
            this.db = db;
            this.st = st;
            template = new Template(this.st);
        }

        public CoursesController()
        {
            db = new LabinatorContext();
            st = new SkyTap();
            template = new Template(st);
        }

        public ActionResult Ajax(DTParameters param)
        {
            return Json(Generic.Ajax<Course>(db.Query<Course>().ToList(), param));
        }

        public JsonResult MachineAjax(DTParameters param)
        {
            DTResult<CourseMachineTemp> result = Generic.Ajax<CourseMachineTemp>(db.Query<CourseMachineTemp>().Where(cmt => cmt.SessionId == param.Session).ToList(), param);
            return Json(result);
        }

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
                PopulateTemp(int.Parse(parameters["Course"]), parameters["Template"], parameters["Session"]);
            }

            response.Add("Status", "Done");
            return Json(response);

        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Template template = new Template(st);
            Course course = null;
            if (id == 0)
            {
                course = new Course() { CourseId = 0, Name = "New", Days = 1, Hours = 8, Template = "", StartTime = new DateTime(2015, 12, 31, 8, 30, 00) };
            }
            else
            {
                course = db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
                if (course == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.Session = Guid.NewGuid().ToString();
            PopulateTemp(id, course.Template, ViewBag.Session);
            List<Template> templates = template.GetList();
            templates.Insert(0, new Template() { name = "Please Select Template...", id = "" });
            ViewBag.Template = new SelectList(templates, "id", "name", course.Template);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "CourseId,Name,Days,Hours,Template,StartTime")] Course course, String Session)
        {
            if (ModelState.IsValid)
            {
                if (course.CourseId == 0)
                {
                    db.Add<Course>(course);
                }
                else
                {
                    db.Update<Course>(course);
                }
                db.SaveChanges();
                List<CourseMachine> cms = db.Query<CourseMachine>().Where(cm => cm.CourseId == course.CourseId).ToList();
                foreach (CourseMachine cm in cms)
                {
                    db.Remove<CourseMachine>(cm);
                }
                List<CourseMachineTemp> cmts = db.Query<CourseMachineTemp>().Where(cm => cm.SessionId == Session).ToList();
                foreach (CourseMachineTemp cmt in cmts)
                {
                    CourseMachine cm = new CourseMachine();
                    cm.CourseId = course.CourseId;
                    cm.VMId = cmt.VMId;
                    cm.IsActive = cmt.IsActive;
                    db.Add<CourseMachine>(cm);
                    db.Remove<CourseMachineTemp>(cmt);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Session = System.Web.HttpContext.Current.Session.SessionID;
            PopulateTemp(course.CourseId, course.Template, ViewBag.Session);
            List<Template> templates = template.GetList();
            templates.Insert(0, new Template() { name = "Please Select Template...", id = "" });
            ViewBag.Template = new SelectList(templates, "id", "name", course.Template);
            return View(course);
        }

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
                int CourseMachineTempId = (parameters["configuration"]);
                CourseMachineTemp courseMachineTemp = db.Query<CourseMachineTemp>().Where(cmt => cmt.CourseMachineTempId == CourseMachineTempId).FirstOrDefault();
                if (courseMachineTemp != null)
                {
                    courseMachineTemp.IsActive = (parameters["active"]);
                    db.Update<CourseMachineTemp>(courseMachineTemp);
                    db.SaveChanges();
                }
            }
            return Json(response);
        }
        [NonAction]
        public void PopulateTemp(int CourseId, string TemplateId, string sessionId)
        {
            Template t = new Template(st);
            if (TemplateId == "")
            {
                ////    t.VMs = new Vm[0];
            }
            else
            {
                t = t.GetTemplate(TemplateId);
            }
            List<CourseMachineTemp> cmts = db.Query<CourseMachineTemp>()
                                            .Where(c => c.SessionId == sessionId)
                                            .ToList();
            if (cmts != null)
            {
                foreach (CourseMachineTemp cmt in cmts)
                {
                    db.Remove<CourseMachineTemp>(cmt);
                }
            }
            if ((CourseId == 0) || (db.Query<Course>().Where(c => c.CourseId == CourseId).First().Template != TemplateId))
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
                        db.Add<CourseMachineTemp>(cmt);
                    }
                }
                db.SaveChanges();
            }
            else
            {
                List<CourseMachine> cms = db.Query<CourseMachine>().Where(cm => cm.CourseId == CourseId).ToList();
                if (cms != null)
                {
                    foreach (CourseMachine cm in cms)
                    {
                        CourseMachineTemp cmt = new CourseMachineTemp();
                        cmt.VMId = cm.VMId;
                        cmt.SessionId = sessionId;
                        cmt.TimeStamp = DateTime.Now;
                        cmt.IsActive = cm.IsActive;
                        db.Add<CourseMachineTemp>(cmt);
                    }
                    db.SaveChanges();
                }
                cmts = db.Query<CourseMachineTemp>()
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
                        db.Add<CourseMachineTemp>(cmt);
                    }
                    else
                    {
                        cmt.VMName = v.name;
                        db.Update<CourseMachineTemp>(cmt);
                    }
                    db.SaveChanges();
                }
                foreach (CourseMachineTemp cmt in cmts)
                {
                    Vm V = t.VMs.Where(v => v.id == cmt.VMId).FirstOrDefault();
                    if (V == null)
                    {
                        db.Remove(cmt);
                    }
                }
                db.SaveChanges();
            }
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Course course = db.Query<Course>().Where(c => c.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            db.Remove<Course>(course);
            List<CourseMachine> cms = db.Query<CourseMachine>().Where(cm => cm.CourseId == id).ToList();
            foreach (CourseMachine cm in cms)
            {
                db.Remove<CourseMachine>(cm);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}