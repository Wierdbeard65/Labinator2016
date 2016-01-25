using System.Collections.Generic;
using Labinator2016.Lib.Headers;
using Labinator2016.Lib.REST;
//using Newtonsoft.Json;
using RestSharp;

namespace Labinator.Lib.REST
{
    public class Vm
    {
        ISkyTap st;
        public string id { get; set; }
        public string name { get; set; }
        public string runstate { get; set; }
        //        public Hardware hardware { get; set; }
        public bool? error { get; set; }
        public object asset_id { get; set; }
        //        public Interface[] interfaces { get; set; }
        public object[] notes { get; set; }
        //        public Label[] labels { get; set; }
        //        public Credential[] credentials { get; set; }
        public bool? desktop_resizable { get; set; }
        public bool? local_mouse_cursor { get; set; }
        public bool? maintenance_lock_engaged { get; set; }
        public string region_backend { get; set; }
        public string created_at { get; set; }
        public bool? can_change_object_state { get; set; }
        public string configuration_url { get; set; }
        public string[] publish_set_refs { get; set; }
        public Vm()
        {
            st = new SkyTap();
        }
        public Vm(ISkyTap st)
        {
            this.st = st;
        }
        //public static List<Vm> allVms()
        //{
        //    var request = new RestRequest(", Method.GET);
        //    Configuration parameters = DLLConfig.Execute<Configuration>(request);
        //    if (parameters == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return parameters.vms.ToList();
        //    }

        public List<Vm> GetList(string ConfigurationId)
        {
            RestRequest request = new RestRequest("v2/configurations/" + ConfigurationId, Method.GET);
            List<Vm> response = st.ExecuteList<Vm>(request);
            return (response);
        }
        public Vm GetTemplate(string VmId)
        {
            RestRequest request = new RestRequest("v2/templates/" + VmId, Method.GET);
            Vm response = st.Execute<Vm>(request);
            return response;
        }

        //public static Boolean PowerSwitch(string VMId)
        //{
        //    String status = Status(VMId);
        //    if (status == "Error")
        //    {
        //        return false;
        //    }
        //    var request = new RestRequest("vms/" + VMId, Method.PUT);
        //    if (status == "stopped")
        //    {
        //        request.AddParameter("runstate", "running");
        //    }
        //    else
        //    {
        //        request.AddParameter("runstate", "halted");
        //    }
        //    Vm parameters = DLLConfig.Execute<Vm>(request);
        //    return (parameters != null);
        //}
        //public static Boolean Suspend(string VMId)
        //{
        //    String status = Status(VMId);
        //    if (status == "Error")
        //    {
        //        return false;
        //    }
        //    var request = new RestRequest("vms/" + VMId, Method.PUT);
        //    if (status == "running")
        //    {
        //        request.AddParameter("runstate", "suspended");
        //    }
        //    Vm parameters = DLLConfig.Execute<Vm>(request);
        //    return (parameters != null);
        //}
        //public static Boolean Start(string VMId)
        //{
        //    String status = Status(VMId);
        //    if (status == "Error")
        //    {
        //        return false;
        //    }
        //    var request = new RestRequest("vms/" + VMId, Method.PUT);
        //    request.AddParameter("runstate", "running");
        //    Vm parameters = DLLConfig.Execute<Vm>(request);
        //    return (parameters != null);
        //}
        //public static string Status(string VMId)
        //{
        //    var request = new RestRequest("vms/" + VMId, Method.GET);
        //    Vm parameters = DLLConfig.Execute<Vm>(request);
        //    if (parameters == null)
        //    {
        //        return "Error";
        //    }
        //    else
        //    {
        //        return parameters.runstate;
        //    }
        //}
        //}
    }
}