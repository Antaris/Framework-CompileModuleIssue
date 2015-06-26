// <copyright company="Fresh Egg Limited" file="Ensure.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;

    /// <summary>
    /// Provides validation methods for values.
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Ensures the given argument value is not null.
        /// </summary>
        /// <param name="argument">The argument value.</param>
        /// <param name="name">The parameter name.</param>
        /// <exception cref="ArgumentNullException">If the argument is null.</exception>
        /// <returns>The argument value</returns>
        public static T ArgumentNotNull<T>(T argument, string name) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }

            return argument;
        }

        /// <summary>
        /// Ensures the given argument value is not null or whitespace.
        /// </summary>
        /// <param name="argument">The argument value.</param>
        /// <param name="name">The parameter name.</param>
        /// <exception cref="ArgumentException">If the argument is null, empty or white space.</exception>
        /// <returns>The argument value</returns>
        public static string ArgumentNotNullOrWhiteSpace(string argument, string name)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException($"The parameter '{name}' cannot be null, empty or white space");
            }

            return argument;
        }
    }
}