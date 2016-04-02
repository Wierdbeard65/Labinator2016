//-----------------------------------------------------------------------
// <copyright file="ISkyTap.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Headers
{
    using System.Collections.Generic;
    using RestSharp;

    /// <summary>
    /// Interface to allow the connection to Sky Tap to be simulated in unit tests.
    /// </summary>
    public interface ISkyTap
    {

        IRestResponse Execute(RestRequest request);
        /// <summary>
        /// Obtains a single object of type T from the Sky Tap REST interface.
        /// </summary>
        /// <typeparam Name="T">The type to obtain.</typeparam>
        /// <param name="request">The request to send</param>
        /// <returns>An object.</returns>
        T Execute<T>(RestRequest request);

        /// <summary>
        /// Obtains a list of objects of type T from the Sky Tap REST interface.
        /// </summary>
        /// <typeparam Name="T">The type to obtain.</typeparam>
        /// <param name="request">The request to send</param>
        /// <returns>A list of objects.</returns>
        List<T> ExecuteList<T>(RestRequest request);
    }
}
