// <copyright company="Fresh Egg Limited" file="IInstanceFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    /// <summary>
    /// Defines the required contract for implementing an instance factory.
    /// </summary>
    public interface IInstanceFactory
    {
        /// <summary>
        /// Creates an instance of the given type.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="P">The constructor parameter type.</typeparam>
        /// <param name="arg">The constructor argument.</param>
        /// <returns>The instance.</returns>
        T Create<T, P>(P arg);

        /// <summary>
        /// Creates an instance of the given type.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="P1">The first constructor parameter type.</typeparam>
        /// <typeparam name="P2">The second constructor parameter type.</typeparam>
        /// <param name="arg1">The first constructor argument.</param>
        /// <param name="arg2">The second constructor argument.</param>
        /// <returns>The instance.</returns>
        T Create<T, P1, P2>(P1 arg1, P2 arg2);
    }
}