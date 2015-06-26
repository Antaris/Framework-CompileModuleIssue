// <copyright company="Fresh Egg Limited" file="SemanticVersion.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a semantic version.
    /// </summary>
    [Serializable]
    public sealed class SemanticVersion : IComparable<SemanticVersion>, IComparable, ISerializable
    {
        private static readonly Regex ParseRegex = new Regex(@"^(?<major>\d+)(\.(?<minor>\d+))?(\.(?<patch>\d+))?(\-(?<pre>[0-9A-Za-z\-\.]+))?(\+(?<build>[0-9A-Za-z\-\.]+))?$", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersion"/> class.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="patch">The patch.</param>
        /// <param name="preRelease">The pre release string.</param>
        /// <param name="build">The build string.</param>
        public SemanticVersion(int major, int minor = 0, int patch = 0, string preRelease = "", string build = "")
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            PreRelease = string.Intern(preRelease ?? string.Empty);
            Build = string.Intern(build ?? string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersion"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public SemanticVersion(Version version = null)
        {
            version = version ?? new Version();

            Major = version.Major;
            Minor = version.Minor;

            if (version.Revision > 0)
            {
                Patch = version.Revision;
            }

            PreRelease = string.Intern(string.Empty);
            Build = string.Intern(version.Build > 0 ? version.Build.ToString() : string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersion"/> class from serialized data.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        private SemanticVersion(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            var version = Parse(info.GetString("SemVar"));
            Major = version.Major;
            Minor = version.Minor;
            Patch = version.Patch;
            PreRelease = version.PreRelease;
            Build = version.Build;
        }

        /// <summary>
        /// Gets the build version string.
        /// </summary>
        public string Build { get; private set; }

        /// <summary>
        /// Gets the major version.
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Gets the patch version.
        /// </summary>
        public int Patch { get; private set; }

        /// <summary>
        /// Gets the pre release version string.
        /// </summary>
        public string PreRelease { get; private set; }

        /// <summary>
        /// Compares the specified versions to resolve order.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>
        /// - 1 if the first version is less than the second; 0 if the versions are equal;, 1 if the
        ///   first version is greater than the second.
        /// </returns>
        public static int Compare(SemanticVersion first, SemanticVersion second)
        {
            if (ReferenceEquals(first, null))
            {
                return ReferenceEquals(second, null) ? 0 : -1;
            }

            return first.CompareTo(second);
        }

        /// <summary>
        /// Tests the specified versions for equality.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>True if the versions are considered equal, otherwise false.</returns>
        public static bool Equals(SemanticVersion first, SemanticVersion second)
        {
            if (ReferenceEquals(first, null))
            {
                return ReferenceEquals(second, null);
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Parses the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="strict">If true, minor and patch versions are required, otherwise they default to 0.</param>
        /// <returns>The semantic version.</returns>
        /// <exception cref="System.ArgumentException">version</exception>
        public static SemanticVersion Parse(string version, bool strict = false)
        {
            var result = ParseInternal(version, strict);
            if (result.Item1 != null)
            {
                return result.Item1;
            }

            throw new ArgumentException(result.Item2, "version");
        }

        /// <summary>
        /// Attempts to parse the given version string as a semantic version.
        /// </summary>
        /// <param name="version">The version string.</param>
        /// <param name="semVer">The semantic version.</param>
        /// <param name="strict">If true, minor and patch versions are required, otherwise they default to 0.</param>
        /// <returns>True if version string was parsed, otherwise false.</returns>
        public static bool TryParse(string version, out SemanticVersion semVer, bool strict = false)
        {
            var result = ParseInternal(version, strict);
            if (result.Item1 != null)
            {
                semVer = result.Item1;
                return true;
            }

            semVer = null;
            return false;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="SemanticVersion"/>.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator SemanticVersion(string version)
        {
            return Parse(version);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(SemanticVersion left, SemanticVersion right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(SemanticVersion left, SemanticVersion right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(SemanticVersion left, SemanticVersion right)
        {
            return Compare(left, right) == 1;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(SemanticVersion left, SemanticVersion right)
        {
            return left == right || left > right;
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(SemanticVersion left, SemanticVersion right)
        {
            return Compare(left, right) == -1;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(SemanticVersion left, SemanticVersion right)
        {
            return left == right || left < right;
        }

        /// <summary>
        /// Returns a copy of the current version with potentially changed fields.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="patch">The patch.</param>
        /// <param name="preRelease">The pre release.</param>
        /// <param name="build">The build.</param>
        /// <returns>The changed version.</returns>
        public SemanticVersion Change(int? major = null, int? minor = null, int? patch = null, string preRelease = null, string build = null)
        {
            return new SemanticVersion(
                major ?? Major,
                minor ?? Minor,
                patch ?? Patch,
                preRelease ?? PreRelease,
                build ?? Build);
        }

        /// <summary>
        /// Compares the specified versions by precedence. This ignores build information.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="obj"/> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="obj"/> . Greater than zero This instance follows <paramref
        /// name="obj"/> in the sort order.
        /// </returns>
        public int CompareByPrecedence(SemanticVersion version)
        {
            if (ReferenceEquals(version, null))
            {
                return 1;
            }

            var r = Major.CompareTo(version.Major);
            if (r != 0)
            {
                return r;
            }

            r = Minor.CompareTo(version.Minor);
            if (r != 0)
            {
                return r;
            }

            r = Patch.CompareTo(version.Patch);
            if (r != 0)
            {
                return r;
            }

            r = CompareComponent(PreRelease, version.PreRelease, true);
            return r;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an
        /// integer that indicates whether the current instance precedes, follows, or occurs in the
        /// same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="obj"/> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="obj"/> . Greater than zero This instance follows <paramref
        /// name="obj"/> in the sort order.
        /// </returns>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as SemanticVersion);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an
        /// integer that indicates whether the current instance precedes, follows, or occurs in the
        /// same position in the sort order as the other object.
        /// </summary>
        /// <param name="version">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="version"/> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="version"/> . Greater than zero This instance follows <paramref
        /// name="version"/> in the sort order.
        /// </returns>
        public int CompareTo(SemanticVersion version)
        {
            if (ReferenceEquals(version, null))
            {
                return 1;
            }

            var r = CompareByPrecedence(version);
            if (r != 0)
            {
                return r;
            }

            r = CompareComponent(Build, version.Build);
            return r;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> , is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        /// otherwise, <c>false</c> .
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as SemanticVersion;
            if (other == null)
            {
                return false;
            }

            return Major == other.Major
                    && Minor == other.Minor
                    && Patch == other.Patch
                    && ReferenceEquals(PreRelease, other.PreRelease)
                    && ReferenceEquals(Build, other.Build);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = Major.GetHashCode();
                result = (result * 31) + Minor.GetHashCode();
                result = (result * 31) + Patch.GetHashCode();
                result = (result * 31) + PreRelease.GetHashCode();
                result = (result * 31) + Build.GetHashCode();

                return result;
            }
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data
        /// needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/> ) for
        /// this serialization.
        /// </param>
        /// <exception cref="System.ArgumentNullException">info</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("SemVer", ToString());
        }

        /// <summary>
        /// Compares semantic version by precedence, ignoring build information.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>True if the version precedence matches.</returns>
        public bool PrecedenceMatches(SemanticVersion version)
        {
            return CompareByPrecedence(version) == 0;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var version = string.Empty + Major + "." + Minor + "." + Patch;
            if (!string.IsNullOrWhiteSpace(PreRelease))
            {
                version += "-" + PreRelease;
            }

            if (!string.IsNullOrWhiteSpace(Build))
            {
                version += "+" + Build;
            }

            return version;
        }

        /// <summary>
        /// Parses the version string.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="strict">If true, minor and patch versions are required, otherwise they default to 0.</param>
        /// <returns>The parsed semantic version, or the error message that should be thrown.</returns>
        internal static Tuple<SemanticVersion, string> ParseInternal(string version, bool strict)
        {
            SemanticVersion result = null;
            var match = ParseRegex.Match(version);
            var culture = CultureInfo.InvariantCulture;

            if (!match.Success)
            {
                return Tuple.Create(result, "Invalid semantic version string.");
            }

            int major = int.Parse(match.Groups["major"].Value, culture);

            var minorMatch = match.Groups["minor"];
            int minor = 0;
            if (minorMatch.Success)
            {
                minor = int.Parse(minorMatch.Value, culture);
            }
            else if (strict)
            {
                return Tuple.Create(result, "Invalid semantic version, no minor version was provided.");
            }

            var patchMatch = match.Groups["patch"];
            int patch = 0;
            if (patchMatch.Success)
            {
                patch = int.Parse(patchMatch.Value, culture);
            }
            else if (strict)
            {
                return Tuple.Create(result, "Invalid semantic version, no patch version was provided.");
            }

            result = new SemanticVersion(major, minor, patch, match.Groups["pre"].Value, match.Groups["build"].Value);
            return Tuple.Create(result, (string)null);
        }

        /// <summary>
        /// Compares two component strings.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="lower">Flag to state whether the first string is considered the lower value.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="obj"/> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="obj"/> . Greater than zero This instance follows <paramref
        /// name="obj"/> in the sort order.
        /// </returns>
        private static int CompareComponent(string first, string second, bool lower = false)
        {
            bool firstEmpty = string.IsNullOrEmpty(first);
            bool secondEmpty = string.IsNullOrEmpty(second);
            if (firstEmpty && secondEmpty)
            {
                return 0;
            }

            if (firstEmpty)
            {
                return lower ? 1 : -1;
            }

            if (secondEmpty)
            {
                return lower ? -1 : 1;
            }

            var firstComponents = first.Split('.');
            var secondComponents = second.Split('.');

            int min = Math.Min(firstComponents.Length, secondComponents.Length);
            for (int i = 0; i < min; i++)
            {
                var firstComponent = firstComponents[i];
                var secondComponent = secondComponents[i];

                int firstNumber, secondNumber;
                bool firstIsNumber = int.TryParse(firstComponent, out firstNumber);
                bool secondIsNumber = int.TryParse(secondComponent, out secondNumber);

                if (firstIsNumber && secondIsNumber)
                {
                    return firstNumber.CompareTo(secondNumber);
                }

                if (firstIsNumber)
                {
                    return -1;
                }

                if (secondIsNumber)
                {
                    return 1;
                }

                var r = string.CompareOrdinal(firstComponent, secondComponent);
                if (r != 0)
                {
                    return r;
                }
            }

            return firstComponents.Length.CompareTo(secondComponents.Length);
        }
    }
}