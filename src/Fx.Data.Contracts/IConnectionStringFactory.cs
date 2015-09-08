// <copyright company="Fresh Egg Limited" file="IConnectionStringFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    /// <summary>
    /// Defines the required contract for implementing a connection string factory.
    /// </summary>
    public interface IConnectionStringFactory
    {
        /// <summary>
        /// Creates a connection string for the given connection name.
        /// </summary>
        /// <param name="name">The name of the connection.</param>
        /// <param name="fallback">The fallback connection string name if the named connection string doesn't exist.</param>
        /// <returns>The connection string.</returns>
        string CreateConnectionString(string name, string fallback = null);
    }
}