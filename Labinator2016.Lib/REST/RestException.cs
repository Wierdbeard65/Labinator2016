//-----------------------------------------------------------------------
// <copyright file="RestException.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.REST
{
    using System;
    using RestSharp;

    /// <summary>
    /// Extends the <see cref="Exception"/> class to allow exceptions to be thrown when something goes wrong with a REST request when talking to Sky Tap.
    /// </summary>
    public class RestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException"/> class.
        /// </summary>
        /// Simply takes the parameters and stores them in the attributes.
        /// <param name="message">The message detailing the problem.</param>
        /// <param name="request">The request that caused the problem,</param>
        /// <param name="response">The response (if any) that was received.</param>
        /// <param name="user">The <see cref="User"/> that attempted the operation.</param>
        /// <param name="apiKey">The API key used for the connection.</param>
        public RestException(string message, RestRequest request, IRestResponse response, string user, string apiKey) : base(message)
        {
            this.Request = request;
            this.Response = response;
            this.User = user;
            this.APIKey = apiKey;
        }

        /// <summary>
        /// Gets the request that caused the problem,
        /// </summary>
        public RestRequest Request { get; }

        /// <summary>
        /// Gets the <see cref="User"/> that attempted the operation.
        /// </summary>
        public string User { get; }

        /// <summary>
        /// Gets the API key used for the connection.
        /// </summary>
        public string APIKey { get; }

        /// <summary>
        /// Gets the response (if any) that was received.
        /// </summary>
        public IRestResponse Response { get; }
    }
}
