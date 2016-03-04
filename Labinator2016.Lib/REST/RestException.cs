using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Labinator2016.Lib.REST
{
    public class RestException : Exception
    {
        public RestRequest Request { get; }
        public string User { get; }
        public string APIKey { get; }
        public IRestResponse Response { get; }
        public RestException(string message, RestRequest request, IRestResponse response, string user, string apiKey) : base(message)
        {
            this.Request = request;
            this.Response = response;
            this.User = user;
            this.APIKey = apiKey;
        }
    }
}
