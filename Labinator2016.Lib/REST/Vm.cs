﻿//-----------------------------------------------------------------------
// <copyright file="Vm.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>

namespace Labinator2016.Lib.REST
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Web.Script.Serialization;
    using Labinator2016.Lib.Headers;
    using RestSharp;

    /// <summary>
    /// Class to represent the data returned by the Sky Tap REST interface for a virtual machine.
    /// </summary>
    public class Vm
    {
        /// <summary>
        /// The handle used for Sky Tap. Allows for unit testing.
        /// </summary>
        private ISkyTap st;

        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the Runstate.
        /// </summary>
        /// <value>
        /// The Runstate.
        /// </value>
        ////public string runstate { get; set; }
        ////        public Hardware hardware { get; set; }
        ////public bool? Error { get; set; }
        ////public object asset_id { get; set; }
        ////public Interface[] interfaces { get; set; }
        ////public object[] notes { get; set; }
        ////////        public Label[] labels { get; set; }
        ////        public Credential[] credentials { get; set; }        

        /// <summary>
        /// Gets or sets a flag indicating if the desktop is resizable..
        /// </summary>
        /// <value>
        /// The desktop resizable flag.
        /// </value>
        public bool? desktop_resizable { get; set; }

        /// <summary>
        /// Gets or sets the local_mouse_cursor.
        /// </summary>
        /// <value>
        /// The local_mouse_cursor.
        /// </value>
        public bool? local_mouse_cursor { get; set; }

        /// <summary>
        /// Gets or sets the maintenance_lock_engaged.
        /// </summary>
        /// <value>
        /// The maintenance_lock_engaged.
        /// </value>
        public bool? maintenance_lock_engaged { get; set; }

        /// <summary>
        /// Gets or sets the Region_backend.
        /// </summary>
        /// <value>
        /// The Region_backend.
        /// </value>
        public string region_backend { get; set; }

        /// <summary>
        /// Gets or sets the created_at.
        /// </summary>
        /// <value>
        /// The created_at.
        /// </value>
        public string created_at { get; set; }

        /// <summary>
        /// Gets or sets the can_change_object_state.
        /// </summary>
        /// <value>
        /// The can_change_object_state.
        /// </value>
        public bool? can_change_object_state { get; set; }

        /// <summary>
        /// Gets or sets the configuration_url.
        /// </summary>
        /// <value>
        /// The configuration_url.
        /// </value>
        public string configuration_url { get; set; }

        /// <summary>
        /// Gets or sets the publish_set_refs.
        /// </summary>
        /// <value>
        /// The publish_set_refs.
        /// </value>
        public string[] publish_set_refs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vm"/> class.
        /// </summary>
        public Vm()
        {
            st = new SkyTap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vm"/> class.
        /// </summary>
        /// <param name="st">The st.</param>
        public Vm(ISkyTap st)
        {
            this.st = st;
        }
        ////public static List<Vm> allVms()
        ////{
        ////    var request = new RestRequest(", Method.GET);
        ////    Configuration parameters = DLLConfig.Execute<Configuration>(request);
        ////    if (parameters == null)
        ////    {
        ////        return null;
        ////    }
        ////    else
        ////    {
        ////        return parameters.VMs.ToList();
        ////    }

        /////// <summary>
        /////// Gets the list.
        /////// </summary>
        /////// <param name="ConfigurationId">The configuration identifier.</param>
        /////// <returns></returns>
        ////public List<Vm> GetList(string ConfigurationId)
        ////{
        ////    RestRequest request = new RestRequest("v2/configurations/" + ConfigurationId, Method.GET);
        ////    List<Vm> response = st.ExecuteList<Vm>(request);
        ////    return (response);
        ////}

        /////// <summary>
        /////// Gets the template.
        /////// </summary>
        /////// <param name="VmId">The vm identifier.</param>
        /////// <returns></returns>
        ////public Vm GetTemplate(string VmId)
        ////{
        ////    RestRequest request = new RestRequest("v2/templates/" + VmId, Method.GET);
        ////    Vm response = this.st.Execute<Vm>(request);
        ////    return response;
        ////}

        public bool PowerSwitch()
        {
            string status = Status();
            if (status == "Error")
            {
                return false;
            }
            var request = new RestRequest("VMs/" + this.id, Method.PUT);
            if (status == "stopped")
            {
                request.AddParameter("Runstate", "running");
            }
            else
            {
                request.AddParameter("Runstate", "halted");
            }
            IRestResponse response = this.st.Execute(request);
            return (response != null);
        }

        public static void SuspendMultiple(object parameter)
        {
            List<Vm> machines = (List<Vm>)parameter;
            foreach(Vm machine in machines)
            {
                machine.Suspend();
            }
        }

        public bool Suspend()
        {
            string status = this.Status();
            if (status == "Error")
            {
                return false;
            }
            if (status == "running")
            {
                var request = new RestRequest("VMs/" + this.id, Method.PUT);
                if (status == "running")
                {
                    request.AddParameter("Runstate", "suspended");
                }
                IRestResponse parameters = this.st.Execute(request);
                return (parameters != null);
            }
            return false;
        }

        public static void StartMultiple(object parameter)
        {
            List<Vm> machines = (List<Vm>)parameter;
            foreach (Vm machine in machines)
            {
                machine.Start();
            }
        }
        public bool Start()
        {
            string status = this.Status();
            if (status == "Error")
            {
                return false;
            }
            if (status != "running")
            {
                var request = new RestRequest("VMs/" + this.id, Method.PUT);
                request.AddParameter("Runstate", "running");
                IRestResponse parameters = this.st.Execute(request);
                return (parameters != null);
            }
            return true;
        }
        public string Status()
        {
            var request = new RestRequest("VMs/" + this.id, Method.GET);
            IRestResponse response = this.st.Execute(request);
            if (response == null)
            {
                return "Error";
            }
            else
            {
                dynamic vm = serializer.DeserializeObject(response.Content);
                return vm["Runstate"];
            }
        }
        ////}
    }
}