// <copyright company="Fresh Egg Limited" file="IRequestInfo.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    /// <summary>
    /// Defines the required contract for implementing a request information model.
    /// </summary>
    public interface IRequestInfo
    {
        /// <summary>
        /// Gets the target host of the request.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the IP address of the connected client.
        /// </summary>
        string IPAddress { get; }

        /// <summary>
        /// Gets whether the request is using TLS/SSL.
        /// </summary>
        bool Secure { get; }

        /// <summary>
        /// Gets the URL of the request.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets the user agent of the request.
        /// </summary>
        string UserAgent { get; }
    }
}