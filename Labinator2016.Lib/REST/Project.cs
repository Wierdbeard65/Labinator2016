namespace Labinator2016.Lib.REST
{
    using System;
    using System.Net;
    using Labinator2016.Lib.Headers;
    using RestSharp;

    public class Project
    {
        ISkyTap st;
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public Boolean show_project_members { get; set; }
        public string auto_add_role_name { get; set; }
        public string region { get; set; }

        public Project()
        {
            st = new SkyTap();
            id = null;
        }

        public Project(ISkyTap st)
        {
            this.st = st;
            id = null;
        }

        public void Add()
        {
            RestRequest request = new RestRequest("projects.json", Method.POST);
//            request.AddParameter("query", "region:" + this.region);
            request.AddParameter("name", this.name);
            Project response = this.st.Execute<Project>(request);
            if (response != default(Project))
            {
                id = response.id;
            }
        }

        public Boolean Delete()
        {
            RestRequest request = new RestRequest("projects/" + id, Method.DELETE);
            IRestResponse response = this.st.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}