using System.Collections.Generic;
using RestSharp;

namespace Labinator2016.Lib.Headers
{
    public interface ISkyTap
    {
        T Execute<T>(RestRequest request);
        List<T> ExecuteList<T>(RestRequest request);
    }
}
