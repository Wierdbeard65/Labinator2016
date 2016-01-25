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

    [Authorize]
    public class DataCentersController : Controller
    {
        private ILabinatorDb db;

        public DataCentersController()
        {
            this.db = new LabinatorContext();
        }

        public DataCentersController(ILabinatorDb db)
        {
            this.db = db;
        }
        public ActionResult Ajax(DTParameters param)
        {
            return Json(Generic.Ajax<DataCenter>(this.db.Query<DataCenter>().ToList(), param));
        }

        // GET: DataCenters
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: DataCenters/Edit/5
        public ActionResult Edit(int id)
        {
            DataCenter dataCenter;
            if (id == 0)
            {
                dataCenter = new DataCenter() { DataCenterId = 0, Name = "New", Timezone = TimeZoneInfo.Local.Id };
            }
            else
            {
                dataCenter = this.db.Query<DataCenter>().Where(dc => dc.DataCenterId == id).FirstOrDefault();
                if (dataCenter == null)
                {
                    return this.HttpNotFound();
                }
            }
            List<TimeZoneInfo> timezoneList = TimeZoneInfo.GetSystemTimeZones().ToList();
            ViewBag.Timezone = new SelectList(timezoneList, "Id", "DisplayName", dataCenter.Timezone);
            return this.View(dataCenter);
        }

        // POST: DataCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DataCenterId,Name,Timezone,Type,GateWayIP")] DataCenter dataCenter)
        {
            if (ModelState.IsValid)
            {
                if (dataCenter.DataCenterId == 0)
                {
                    this.db.Add<DataCenter>(dataCenter);
                }
                else
                {
                    this.db.Update<DataCenter>(dataCenter);
                }
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(dataCenter);
        }

        // GET: DataCenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataCenter dataCenter = this.db.Query<DataCenter>().Where(dc => dc.DataCenterId == id).FirstOrDefault();
            if (dataCenter == null)
            {
                return this.HttpNotFound();
            }
            return this.View(dataCenter);
        }

        // POST: DataCenters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            DataCenter dataCenter = this.db.Query<DataCenter>().Where(dc => dc.DataCenterId == id).FirstOrDefault();
            this.db.Remove<DataCenter>(dataCenter);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
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
