// <copyright company="Fresh Egg Limited" file="FilterGroupModel.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a group of filters in a query.
    /// </summary>
    public sealed class FilterGroupModel : FilterModel, IEquatable<FilterGroupModel>, IComparable<FilterGroupModel>
    {
        private readonly int _cachedHashCode;

        /// <summary>
        /// Initialises a new instance of <see cref="FilterGroupModel"/>
        /// </summary>
        /// <param name="filters">The set of filters.</param>
        /// <param name="logic">The logic to combine filters.</param>
        public FilterGroupModel(IEnumerable<FilterModel> filters, FilterGroupLogic logic)
        {
            Filters = new ReadOnlyCollection<FilterModel>(new List<FilterModel>(filters ?? Enumerable.Empty<FilterModel>()));
            Logic = logic;

            _cachedHashCode = CalculateHashCode();
        }

        /// <summary>
        /// Gets the set of query filters.
        /// </summary>
        public ReadOnlyCollection<FilterModel> Filters { get; private set; }

        /// <summary>
        /// Gets the group logic.
        /// </summary>
        public FilterGroupLogic Logic { get; private set; }

        /// <inheritdoc />
        public int CompareTo(FilterGroupModel other)
        {
            if (other == null)
            {
                return -1;
            }

            if (Logic < other.Logic)
            {
                return -1;
            }
            else if (Logic > other.Logic)
            {
                return 1;
            }

            int max = Math.Max(Filters.Count, other.Filters.Count);
            for (int i = 0; i < max; i++)
            {
                var filterA = Filters.ElementAtOrDefault(i);
                var filterB = other.Filters.ElementAtOrDefault(i);

                if (filterA != null && filterB != null)
                {
                    int result = filterA.CompareTo(filterB);
                    if (result != 0)
                    {
                        return result;
                    }
                }
                else if (filterA != null)
                {
                    return -1;
                }
                else if (filterB != null)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <inheritdoc />
        public override int CompareTo(FilterModel other)
        {
            return CompareTo(other as FilterGroupModel);
        }

        /// <inheritdoc />
        public bool Equals(FilterGroupModel other)
        {
            if (other == null)
            {
                return false;
            }

            if (Logic != other.Logic)
            {
                return false;
            }

            if (Filters.Count < other.Filters.Count || Filters.Count > other.Filters.Count)
            {
                return false;
            }

            for (int i = 0; i < Filters.Count; i++)
            {
                var filterA = Filters[i];
                var filterB = other.Filters[i];

                if (!filterA.Equals(filterB))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(FilterModel other)
        {
            return Equals(other as FilterGroupModel);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as FilterGroupModel);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _cachedHashCode;
        }

        /// <summary>
        /// Calculates the hashcode for this filter group.
        /// </summary>
        /// <returns>The hashcode for this group.</returns>
        private int CalculateHashCode()
        {
            unchecked
            {
                int hash = 17;

                foreach (var filter in Filters)
                {
                    if (filter != null)
                    {
                        hash = hash * 23 + filter.GetHashCode();    
                    }
                    else
                    {
                        hash = hash * 23 - 1;
                    }
                }

                return hash;
            }
        }
    }
}