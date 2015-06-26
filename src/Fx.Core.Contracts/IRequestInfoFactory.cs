// <copyright company="Fresh Egg Limited" file="IRequestInfoFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    /// <summary>
    /// Defines the required contract for implementing a request info factory.
    /// </summary>
    public interface IRequestInfoFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IRequestInfo"/> for the current request.
        /// </summary>
        /// <returns>The request info.</returns>
        IRequestInfo CreateRequestInfo();
    }
}