// <copyright company="Fresh Egg Limited" file="ILocalisedStringFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Localisation
{
    using System.Globalization;

    /// <summary>
    /// Defines the required contract for implementing a localised string factory.
    /// </summary>
    public interface ILocalisedStringFactory
    {
        /// <summary>
        /// Creates the localised string.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="fallback">The fallback.</param>
        /// <param name="culture">[Optional] The culture.</param>
        /// <returns>The localised string.</returns>
        LocalisedString CreateLocalisedString(string code, string fallback, CultureInfo culture = null);
    }
}