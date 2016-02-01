namespace Labinator2016.Lib.REST
{
    using System;
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
        public Project()
        {
            st = new SkyTap();
        }
        public Project(ISkyTap st)
        {
            this.st = st;
        }
        public String Add(String Name)
        {
            String Return = null;
            RestRequest request = new RestRequest("projects.json", Method.POST);
            request.AddParameter("name", Name);
            Project response = st.Execute<Project>(request);
            if (response != default(Project))
            {
                Return = response.id;
            }
            return Return;
        }
        //public static Boolean Delete(String Project)
        //{
        //    RestRequest request = new RestRequest("projects/" + Project, Method.DELETE);
        //    var response = DLLConfig.Client.Execute(request);

        //    return response.StatusCode == HttpStatusCode.OK;
        //}
    }
}