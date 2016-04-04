//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.REST
{
    using System.Net;
    using System.Web.Script.Serialization;
    using Labinator2016.Lib.Headers;
    using RestSharp;
    using Utilities;

    /// <summary>
    /// Class that corresponds to a configuration in Sky Tap. Used to encapsulate the various REST parameters.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// A general purpose <see cref="JavaScriptSerializer"/> object used do convert to / from JSON.
        /// </summary>
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Pointer to the Sky Tap access point.
        /// </summary>
        private ISkyTap st;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// Used in production.
        public Configuration()
        {
            this.st = new SkyTap();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// Used when Unit Testing
        /// <param name="st">The handle to the fake Sky Tap</param>
        public Configuration(ISkyTap st)
        {
            this.st = st;
        }

        /// <summary>
        /// Gets or sets the configuration identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the identifier for the backbone network in this configuration.
        /// </summary>
        public string BackboneId
        {
            get
            {
                string result = null;
                RestRequest request = new RestRequest("configurations/" + this.Id, Method.GET);
                try
                {
                    IRestResponse response = this.st.Execute(request);
                    dynamic config = serializer.DeserializeObject(response.Content);
                    foreach (dynamic network in config["networks"])
                    {
                        if (network["Name"] == "Backbone")
                        {
                            result = network["ID"];
                        }
                    }
                }
                catch (RestException e)
                {
                    throw e;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a string representing the public IP address of the Router in this configuration.
        /// </summary>
        public string PublicIP
        {
            get
            {
                string result = null;
                var request = new RestRequest("configurations/" + this.Id, Method.GET);
                try
                {
                    IRestResponse response = this.st.Execute(request);
                    dynamic configuration = serializer.DeserializeObject(response.Content);
                    foreach (dynamic machine in configuration.vms)
                    {
                        if (machine.name == "TestRouter")
                        {
                            foreach (dynamic nic in machine.interfaces)
                            {
                                if (nic.network_name == "Backbone")
                                {
                                    result = nic.ip.ToString();
                                }
                            }
                        }
                    }
                }
                catch (RestException e)
                {
                    throw e;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the URL used to access this configuration.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the configuration Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a string indicating an Error condition.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets a description of the configuration.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a parameter the use of which I have no idea.
        /// </summary>
        public object Suspend_on_idle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this configuration has routes elsewhere.
        /// </summary>
        public bool Routable { get; set; }

        /// <summary>
        /// Gets or sets an array of all of the Virtual Machines in this configuration.
        /// </summary>
        public Vm[] VMs { get; set; }

        /// <summary>
        /// Gets or sets an array describing all the networks in this configuration.
        /// </summary>
        public Network[] Networks { get; set; }

        /// <summary>
        /// Gets or sets the version of the lock.
        /// </summary>
        public string Lockversion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the smart client should be used.
        /// </summary>
        public bool Use_smart_client { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether internet access from this configuration should be disabled.
        /// </summary>
        public bool Disable_internet { get; set; }

        /// <summary>
        /// Gets or sets the Region used for this configuration.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the Region backend for this configuration.
        /// </summary>
        public string Region_backend { get; set; }

        /// <summary>
        /// Gets or sets the Sky Tap user who owns this Configuration.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets something of which I have no idea
        /// </summary>
        public Publish_Sets[] Publish_sets { get; set; }

        /// <summary>
        /// Gets the current state of the Configuration in Sky Tap.
        /// </summary>
        public string Runstate
        {
            get
            {
                var request = new RestRequest("configurations/" + this.Id, Method.GET);
                string retVal;
                try
                {
                    IRestResponse config = this.st.Execute(request);
                    dynamic response = serializer.DeserializeObject(config.Content);
                    retVal = response.runstate;
                }
                catch (RestException e)
                {
                    retVal = "unable to connect";
                }

                return retVal.ToLower();
            }
        }

        /// <summary>
        /// Gets an integer representing the unique sub-net for this configuration.
        /// </summary>
        /// Calculated based on the Sky Tap identifier for the configuration.
        private int Subnet
        {
            get
            {
                int numericIP = int.Parse(this.Id);
                numericIP = numericIP << 3;
                numericIP = numericIP & 0x00ffffff;
                numericIP = numericIP | 0x0a000000;
                return numericIP;
            }
        }

        /// <summary>
        /// Removes the configuration from Sky Tap, deleting all the virtual machines.
        /// </summary>
        /// <returns>A Status code to indicate success</returns>
        public bool Delete()
        {
            RestRequest request = new RestRequest("configurations/" + this.Id, Method.DELETE);
            IRestResponse response = this.st.Execute(request);
            this.Id = null;
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Creates the Configuration (Environment) in Sky Tap.
        /// </summary>
        /// <param name="project">The Sky Tap Project to place the Configuration in.</param>
        /// <param name="template">The Sky Tap template to use.</param>
        /// <param name="gatewayBackboneId">The identifier for the backbone network containing the Gateway</param>
        /// <param name="region">The Region in which to create the configuration.</param>
        /// <returns>A string containing the Sky Tap identification for the new configuration.</returns>
        public string Add(string project, string template, string gatewayBackboneId, string region)
        {
            string retVal = null;
            RestRequest request = new RestRequest("configurations.json", Method.POST);
            request.AddParameter("template_id", template);
            request.AddParameter("Name", this.Name);
            request.AddParameter("query", "Region:" + region);
            try
            {
                Configuration response = this.st.Execute<Configuration>(request);
                if (response != default(Configuration))
                {
                    this.Id = response.Id;
                    while (this.Runstate == "busy")
                    {
                    }

                    RestRequest addConfigurationToProjectRequest = new RestRequest("projects/" + project + "/configurations/" + this.Id, Method.POST);
                    IRestResponse response2 = this.st.Execute(addConfigurationToProjectRequest);
                    int numericIP = this.Subnet;
                    string textSubnet = IPUtils.NumericToStringIP(numericIP);
                    numericIP++;
                    string textIP = IPUtils.NumericToStringIP(numericIP);
                    numericIP += 5;
                    string textGateway = IPUtils.NumericToStringIP(numericIP);
                    if (this.BackboneId != null)
                    {
                        while (this.Runstate == "busy")
                        {
                        }

                        RestRequest updateConfigIPRequest = new RestRequest("configurations/" + this.Id + "/networks/" + this.BackboneId + ".json", Method.PUT);
                        updateConfigIPRequest.AddParameter("Subnet", textSubnet + "/29");
                        updateConfigIPRequest.AddParameter("Subnet_addr", textSubnet);
                        updateConfigIPRequest.AddParameter("Subnet_size", 29);
                        updateConfigIPRequest.AddParameter("Gateway", textGateway);
                        IRestResponse response3 = this.st.Execute(updateConfigIPRequest);
                        RestRequest createtunnelRequest = new RestRequest("Tunnels.json", Method.POST);
                        createtunnelRequest.AddParameter("source_network_id", this.BackboneId);
                        createtunnelRequest.AddParameter("target_network_id", gatewayBackboneId);
                        IRestResponse response4 = this.st.Execute(createtunnelRequest);
                        retVal = this.Id;
                    }
                }
            }
            catch (RestException e)
            {
                this.Delete();
            }

            return retVal;
        }

        ////public void Start()
        ////{
        ////    var request = new RestRequest("configurations/" + this.Id, Method.PUT);
        ////    request.AddParameter("Runstate", "running");
        ////    this.st.Execute(request);
        ////}

        ////public void Suspend()
        ////{
        ////    var request = new RestRequest("configurations/" + this.Id, Method.PUT);
        ////    request.AddParameter("Runstate", "suspended");
        ////    this.st.Execute(request);
        ////}
    }
}