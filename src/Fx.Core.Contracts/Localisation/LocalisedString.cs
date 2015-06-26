// <copyright company="Fresh Egg Limited" file="LocalisedString.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Localisation
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a localised string.
    /// </summary>
    public class LocalisedString
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalisedString"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="text">The text.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="isFallback">Flag to state whether the localised text wasn't resolved, and this instance is using a fallback text.</param>
        public LocalisedString(string code, string text, CultureInfo culture, bool isFallback)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(code, nameof(code));
            Ensure.ArgumentNotNullOrWhiteSpace(text, nameof(text));
            Ensure.ArgumentNotNull(culture, nameof(culture));

            Code = string.Intern(code);
            Text = text;
            Culture = culture;
            IsFallback = isFallback;
        }

        /// <summary>
        /// Gets the localised string system code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is fallback text.
        /// </summary>
        public bool IsFallback { get; private set; }

        /// <summary>
        /// Gets the localised text.
        /// </summary>
        public string Text { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Text;
        }
    }
}