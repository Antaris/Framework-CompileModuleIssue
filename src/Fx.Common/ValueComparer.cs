// <copyright company="Fresh Egg Limited" file="ValueComparer.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Provides comparison services for types.
    /// </summary>
    /// <typeparam name="T">The model type.</typeparam>
    public class ValueComparer<T> : IComparer<T>, IEqualityComparer<T> where T : class
    {
        /// <summary>
        /// Initialises a new instance of <see cref="ValueComparer{T}"/>
        /// </summary>
        /// <param name="props">The set of property fetchers.</param>
        public ValueComparer(params Func<T, object>[] props)
        {
            Properties = new ReadOnlyCollection<Func<T, object>>(props);
        }

        /// <summary>
        /// Gets the set of property fetcher delegates.
        /// </summary>
        public ReadOnlyCollection<Func<T, object>> Properties { get; private set; }

        /// <inheritdoc />
        public int Compare(T x, T y)
        {
            var comparer = Comparer.DefaultInvariant;

            foreach (var prop in Properties)
            {
                var result = comparer.Compare(x, y);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        /// <inheritdoc />
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return Properties.All(p => Equals(p(x), p(y)));
        }

        /// <inheritdoc />
        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }

            unchecked
            {
                int hash = 17;

                foreach (var prop in Properties)
                {
                    object value = prop(obj);
                    if (value == null)
                    {
                        hash = hash * 23 - 1;
                    }
                    else
                    {
                        hash = hash * 23 + value.GetHashCode();
                    }
                }

                return hash;
            }
        }
    }
}