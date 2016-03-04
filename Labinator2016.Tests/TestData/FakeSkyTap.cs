
namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.REST;
    using RestSharp;

    class FakeSkyTap : ISkyTap
    {
        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }
        public void Dispose() { }

        public void AddSet<T>(IQueryable<T> objects)
        {
            Sets.Add(typeof(T), objects);
        }
        public T Execute<T>(RestRequest request)
        {
            if (request.Method == Method.POST)
            {
                string name = request.Parameters.Where(p => p.Name == "Name").First().Value.ToString();
                Added.Add(name);
                var type = typeof(T).ToString();
                if (type == typeof(Project).ToString())
                {
                    Project instance = new Project() { id = "11111111" };
                    return (T)Convert.ChangeType(instance, typeof(T));

                }
            }
            return (Sets[typeof(T)] as IQueryable<T>).First(); ;
        }
        public List<T> ExecuteList<T>(RestRequest request)
        {
            return (Sets[typeof(T)] as IQueryable<T>).ToList();
        }
        public void Add<T>(T entity) where T : class
        {
            Added.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            Updated.Add(entity);
        }
        public void Remove<T>(T entity) where T : class
        {
            Removed.Add(entity);
        }
        public IRestResponse Execute(RestRequest request)
        {
            return new RestResponse();
        }
        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
        public List<object> Added = new List<object>();
        public List<object> Updated = new List<object>();
        public List<object> Removed = new List<object>();
    }
}
