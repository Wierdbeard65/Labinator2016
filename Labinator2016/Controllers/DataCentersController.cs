//-----------------------------------------------------------------------
// <copyright file="DataCentersController.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// Version: 1.1 - Logging added.
/// Version: 1.2 - Gateway storage added.
/// </summary>
namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.Models;
    using Labinator2016.Lib.REST;
    using Labinator2016.ViewModels.DatatablesViewModel;
    using RestSharp;

    /// <summary>
    /// Back-end processing for all DataCenter-related work and pages.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DataCentersController : Controller
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCentersController"/> class for use live.
        /// </summary>
        public DataCentersController()
        {
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCentersController"/> class for use in testing.
        /// </summary>
        /// <param name="db">The Fake database</param>
        public DataCentersController(ILabinatorDb db)
        {
            this.db = db;
        }

        /// <summary>
        /// Provides a response to an AJAX request for a list of DataCenters.
        /// </summary>
        /// <param name="param">The filter, sort and paging configuration from the DataTable</param>
        /// <returns>A JSON response with the requested data</returns>
        public ActionResult Ajax(DTParameters param)
        {
            return this.Json(Generic.Ajax<DataCenter>(this.db.Query<DataCenter>().ToList(), param));
        }

        /// <summary>
        /// When the client sends an AJAX request for a list of <see cref="Configuration"/>s for a specific Region, this Action responds
        /// </summary>
        /// When setting up a <see cref="DataCenter"/>, a machine (<see cref="Configuration"/>) must be identified which hosts the Spark Gateway. The response
        /// to the AJAX request is used to populate the Select List.
        /// <param name="region">The <see cref="Region"/> we are working with</param>
        /// <returns>A JSON encoded list of <see cref="Configuration"/>s</returns>
        public ActionResult ConfigurationAjax(string region)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, string> reply = new Dictionary<string, string>();
            SkyTap st = new SkyTap();
            if (region != string.Empty)
            {
                RestRequest request = new RestRequest("v2/configurations", Method.GET);
                request.AddParameter("query", "Region:" + region);
                IRestResponse restResponse = st.Execute(request);
                dynamic response = serializer.DeserializeObject(restResponse.Content);
                foreach (dynamic configuration in response)
                {
                    reply.Add(configuration["ID"], configuration["name"]);
                }
            }

            return this.Json(reply);
        }

        /// <summary>
        /// Provides a list of all DataCenters currently in the system.
        /// </summary>
        /// <returns>The Index View.</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The first stage in editing a DataCenter.
        /// </summary>
        /// <param name="id">The DataCenterID of the DataCenter to edit. Zero indicates "new".</param>
        /// <returns>The edit view.</returns>
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

            List<string> regions = Region.regions;
            List<string> centers = this.db.Query<DataCenter>().Select(d => d.Region).ToList();
            regions = regions.Where(r => !centers.Contains(r)).ToList();
            regions.Sort();
            List<SelectListItem> regionList = new List<SelectListItem>();
            foreach (string region in regions)
            {
                regionList.Add(new SelectListItem() { Text = region, Value = region });
            }

            regionList.Insert(0, new SelectListItem() { Text = "Select Region....", Value = string.Empty, Selected = true });
            ViewBag.Region = new SelectList(regionList, "Value", "Text", dataCenter.Region);
            List<TimeZoneInfo> timezoneList = TimeZoneInfo.GetSystemTimeZones().ToList();
            ViewBag.Timezone = new SelectList(timezoneList, "Id", "DisplayName", dataCenter.Timezone);
            return this.View(dataCenter);
        }

        /// <summary>
        /// Writes the changes made to a DataCenter back to the database.
        /// </summary>
        /// <param name="dataCenter">The DataCenter object returned from the browser.</param>
        /// <returns>A redirection back to the list of DataCenters.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DataCenterId,Name,Timezone,Type,GateWayIP,GateWayId,Region")] DataCenter dataCenter)
        {
            if (ModelState.IsValid)
            {
                if (dataCenter.GateWayId != string.Empty)
                {
                    Configuration backbone = new Configuration();
                    backbone.Id = dataCenter.GateWayId;
                    try
                    {
                        dataCenter.GateWayBackboneId = backbone.BackboneId;
                    }
                    catch (RestException e)
                    {
                    }
                }

                if (dataCenter.DataCenterId == 0)
                {
                    if (dataCenter.Region != string.Empty)
                    {
                        this.db.Add<DataCenter>(dataCenter);
                        Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.create, Detail = "DataCenter " + dataCenter.Name + " created." });
                    }
                    else
                    {
                        return this.Edit(dataCenter.DataCenterId);
                    }
                }
                else
                {
                    this.db.Update<DataCenter>(dataCenter);
                    Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.update, Detail = "DataCenter " + dataCenter.Name + " updated." });
                }

                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.Edit(dataCenter.DataCenterId);
        }

        /// <summary>
        /// Performs the first part of the two-stage deletion of a DataCenter.
        /// </summary>
        /// <param name="id">The DataCenterID of the datacenter to delete.</param>
        /// <returns>The confirmation view</returns>
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

        /// <summary>
        /// Performs the actual deletion of a Datacenter when confirmed by the User.
        /// </summary>
        /// <param name="id">The DataCenterID of the Datacenter to delete.</param>
        /// <returns>A redirection back to the list of DataCenters.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            DataCenter dataCenter = this.db.Query<DataCenter>().Where(dc => dc.DataCenterId == id).FirstOrDefault();
            this.db.Remove<DataCenter>(dataCenter);
            this.db.SaveChanges();
            Log.Write(this.db, ControllerContext.HttpContext, new Log() { Message = LogMessages.delete, Detail = "DataCenter " + dataCenter.Name + " deleted." });
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
