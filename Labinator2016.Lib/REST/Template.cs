using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labinator2016.Lib.REST;
using Labinator2016.Lib.Headers;
using RestSharp;

namespace Labinator2016.Lib.REST
{
    public class Template
    {
        ISkyTap st;
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string region_backend { get; set; }
        public List<Vm> VMs { get; set; }
        public Template()
        {
            st = new SkyTap();
        }
        public Template(ISkyTap st)
        {
            this.st = st;
        }
        public List<Template> GetList()
        {
            RestRequest request = new RestRequest("v2/templates", Method.GET);
            request.AddParameter("scope", "company");
            request.AddParameter("count", "100");
            request.AddParameter("sort", "Name");
            List<Template> response = st.ExecuteList<Template>(request);
            return response;
        }
        public Template GetTemplate(string TemplateId)
        {
            RestRequest request = new RestRequest("v2/templates/" + TemplateId, Method.GET);
            Template response = st.Execute<Template>(request);
            return response;
        }
    }
}

