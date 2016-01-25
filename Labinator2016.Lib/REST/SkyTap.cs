using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using Labinator2016.Lib.Headers;
using Labinator2016.Lib.Models;
using Labinator2016.Lib.Utilities;
using RestSharp;
using RestSharp.Authenticators;

namespace Labinator2016.Lib.REST
{
    public class SkyTap : ISkyTap
    {
        const int MAX_RETRIES = 10;
        public static string SkyTapUser { get; set; }
        public static string SkyTapAPIKey { get; set; }
        public static string SkyTapRegion { get; set; }
        public static RestClient Client { get; set; }
        private static JavaScriptSerializer serializer;
        private static System.Diagnostics.EventLog eventLog;
        private static ILabinatorDb db;
        static SkyTap()
        {
            db = new LabinatorContext();
            serializer = new JavaScriptSerializer();
            SkyTapUser = "paul.simpson@inin.com";
            SkyTapAPIKey = "c4246ea631bf8a6c97d85997a3494082251ece3e";
            SkyTapRegion = "US-West";
            Client = new RestClient();
            Client.BaseUrl = new Uri("https://cloud.skytap.com");
            Client.Authenticator = new HttpBasicAuthenticator(SkyTapUser, SkyTapAPIKey);
            eventLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists("Skytapinator_WebApp"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource(
            //        "Skytapinator_WebApp", "Skytapinator");
            //}
            eventLog.Source = "Skytapinator_Service";
            eventLog.Log = "Skytapinator";
        }
        public T Execute<T>(RestRequest request)
        {
            int counter = 0;
            request.AddParameter("query", "region:" + SkyTapRegion);
            var response = SkyTap.Client.Execute(request);
            while (response.StatusCode != HttpStatusCode.OK && counter < MAX_RETRIES)
            {
                counter++;
                EventLog.LogE("REST request Failed. Retry attempt " + counter + "\r\n" +
                     "Parameters  : " + request.Parameters.ToString(),
                     System.Diagnostics.EventLogEntryType.Error,
                     EventIds.ConfigCreateFailure
                );
                response = SkyTap.Client.Execute(request);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
               EventLog.LogE("REST request Failed. Giving up.\r\n" +
                     "Parameters  : " + request.Parameters.ToString(),
                     System.Diagnostics.EventLogEntryType.Error,
                     EventIds.ConfigCreateFailure
                );
                return default(T);
            }
            return serializer.Deserialize<T>(response.Content);
        }
        public List<T> ExecuteList<T>(RestRequest request)
        {
            int counter = 0;
            request.AddParameter("query", "region:" + SkyTapRegion);
            var response = SkyTap.Client.Execute(request);
            while (response.StatusCode != HttpStatusCode.OK && counter < MAX_RETRIES)
            {
                counter++;
                EventLog.LogE("REST request Failed. Retry attempt " + counter + "\r\n" +
                     "Parameters  : " + request.Parameters.ToString(),
                     System.Diagnostics.EventLogEntryType.Error,
                     EventIds.ConfigCreateFailure
                );
                response = SkyTap.Client.Execute(request);
            }
            List<T> reply = new List<T>();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                EventLog.LogE("REST request Failed. Giving up.\r\n" +
                     "Parameters  : " + request.Parameters.ToString(),
                     System.Diagnostics.EventLogEntryType.Error,
                     EventIds.ConfigCreateFailure
                );
            }
            else
            {
                IEnumerable<T> array = serializer.Deserialize<IEnumerable<T>>(response.Content);
                reply = array.ToList();
            }
            return reply;

        }
    }
}