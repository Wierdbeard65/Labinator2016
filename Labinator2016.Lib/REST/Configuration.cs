namespace Labinator2016.Lib.REST
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Script.Serialization;
    using Labinator2016.Lib.Headers;
    using RestSharp;
    using Utilities;
    public class Configuration
    {

        public Configuration()
        {
            this.st = new SkyTap();
        }

        public Configuration(ISkyTap st)
        {
            this.st = st;
        }

        private ISkyTap st;

        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        private int Subnet
        {
            get
            {
                int numericIP = int.Parse(Id);
                numericIP = numericIP << 3;
                numericIP = numericIP & 0x00ffffff;
                numericIP = numericIP | 0x0a000000;
                return numericIP;
            }
        }
        public string Id { get; set; }

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
                        if (network["name"] == "Backbone")
                        {
                            result = network["id"];
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

        public string PublicIP
        {
            get
            {
                string result = null;
                var request = new RestRequest("configurations/" + this.Id, Method.GET);
                try {
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
                catch(RestException e)
                {
                    throw e;
                }
                return result;
            }
        }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Error { get; set; }

        public string Description { get; set; }

        public object Suspend_on_idle { get; set; }

        public bool Routable { get; set; }

        public Vm[] vms { get; set; }

        public Network[] networks { get; set; }

        public string lockversion { get; set; }

        public bool use_smart_client { get; set; }

        public bool disable_internet { get; set; }

        public string region { get; set; }

        public string region_backend { get; set; }

        public string owner { get; set; }

        public Publish_Sets[] publish_sets { get; set; }

        public string Add(string project, string template, string GatewayBackboneId, string region)
        {
            string retVal = null;
            RestRequest request = new RestRequest("configurations.json", Method.POST);
            request.AddParameter("template_id", template);
            request.AddParameter("Name", this.Name);
            request.AddParameter("query", "region:" + region);
            try
            {
                Configuration response = this.st.Execute<Configuration>(request);
                if (response != default(Configuration))
                {
                    this.Id = response.Id;
                    while (this.Runstate == "busy") ;
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
                        while (this.Runstate == "busy") ;
                        RestRequest updateConfigIPRequest = new RestRequest("configurations/" + this.Id + "/networks/" + BackboneId + ".json", Method.PUT);
                        updateConfigIPRequest.AddParameter("subnet", textSubnet + "/29");
                        updateConfigIPRequest.AddParameter("subnet_addr", textSubnet);
                        updateConfigIPRequest.AddParameter("subnet_size", 29);
                        updateConfigIPRequest.AddParameter("gateway", textGateway);
                        IRestResponse response3 = this.st.Execute(updateConfigIPRequest);
                        RestRequest createtunnelRequest = new RestRequest("tunnels.json", Method.POST);
                        createtunnelRequest.AddParameter("source_network_id", this.BackboneId);
                        createtunnelRequest.AddParameter("target_network_id", GatewayBackboneId);
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

        public bool Delete()
        {
            RestRequest request = new RestRequest("configurations/" + this.Id, Method.DELETE);
            IRestResponse response = this.st.Execute(request);
            this.Id = null;
            return response.StatusCode == HttpStatusCode.OK;
        }

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

        public void Start()
        {
            var request = new RestRequest("configurations/" + this.Id, Method.PUT);
            request.AddParameter("Runstate", "running");
            this.st.Execute(request);
        }

        public void Suspend()
        {
            var request = new RestRequest("configurations/" + this.Id, Method.PUT);
            request.AddParameter("Runstate", "suspended");
            this.st.Execute(request);
        }
    }
}