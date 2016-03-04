//-----------------------------------------------------------------------
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
        public string runstate { get; set; }
        ////        public Hardware hardware { get; set; }
        public bool? error { get; set; }
        public object asset_id { get; set; }
        public Interface[] interfaces { get; set; }
        public object[] notes { get; set; }
        ////        public Label[] labels { get; set; }
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
        /// Gets or sets the region_backend.
        /// </summary>
        /// <value>
        /// The region_backend.
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
        /// <param Name="st">The st.</param>
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
        ////        return parameters.vms.ToList();
        ////    }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param Name="ConfigurationId">The configuration identifier.</param>
        /// <returns></returns>
        public List<Vm> GetList(string ConfigurationId)
        {
            RestRequest request = new RestRequest("v2/configurations/" + ConfigurationId, Method.GET);
            List<Vm> response = st.ExecuteList<Vm>(request);
            return (response);
        }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <param Name="VmId">The vm identifier.</param>
        /// <returns></returns>
        public Vm GetTemplate(string VmId)
        {
            RestRequest request = new RestRequest("v2/templates/" + VmId, Method.GET);
            Vm response = this.st.Execute<Vm>(request);
            return response;
        }

        ////public static Boolean PowerSwitch(string VMId)
        ////{
        ////    String status = Status(VMId);
        ////    if (status == "Error")
        ////    {
        ////        return false;
        ////    }
        ////    var request = new RestRequest("vms/" + VMId, Method.PUT);
        ////    if (status == "stopped")
        ////    {
        ////        request.AddParameter("Runstate", "running");
        ////    }
        ////    else
        ////    {
        ////        request.AddParameter("Runstate", "halted");
        ////    }
        ////    Vm parameters = DLLConfig.Execute<Vm>(request);
        ////    return (parameters != null);
        ////}
        ////public static Boolean Suspend(string VMId)
        ////{
        ////    String status = Status(VMId);
        ////    if (status == "Error")
        ////    {
        ////        return false;
        ////    }
        ////    var request = new RestRequest("vms/" + VMId, Method.PUT);
        ////    if (status == "running")
        ////    {
        ////        request.AddParameter("Runstate", "suspended");
        ////    }
        ////    Vm parameters = DLLConfig.Execute<Vm>(request);
        ////    return (parameters != null);
        ////}
        ////public static Boolean Start(string VMId)
        ////{
        ////    String status = Status(VMId);
        ////    if (status == "Error")
        ////    {
        ////        return false;
        ////    }
        ////    var request = new RestRequest("vms/" + VMId, Method.PUT);
        ////    request.AddParameter("Runstate", "running");
        ////    Vm parameters = DLLConfig.Execute<Vm>(request);
        ////    return (parameters != null);
        ////}
        ////public static string Status(string VMId)
        ////{
        ////    var request = new RestRequest("vms/" + VMId, Method.GET);
        ////    Vm parameters = DLLConfig.Execute<Vm>(request);
        ////    if (parameters == null)
        ////    {
        ////        return "Error";
        ////    }
        ////    else
        ////    {
        ////        return parameters.Runstate;
        ////    }
        ////}
        ////}
    }
}