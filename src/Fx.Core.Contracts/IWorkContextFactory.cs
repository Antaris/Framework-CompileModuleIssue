// <copyright company="Fresh Egg Limited" file="IWorkContextFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    /// <summary>
    /// Defines the required contract for implementing a work context factory.
    /// </summary>
    public interface IWorkContextFactory
    {
        /// <summary>
        /// Creates the work context.
        /// </summary>
        /// <returns>The work context instance.</returns>
        IWorkContext CreateWorkContext();
    }
}