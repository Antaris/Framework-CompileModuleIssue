// <copyright company="Fresh Egg Limited" file="QueryModel.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents the model for a query.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public sealed class QueryModel<T>
    {
        /// <summary>
        /// Initialises a new instance of <see cref="QueryModel"/>
        /// </summary>
        /// <param name="filters">The set of query filters.</param>
        /// <param name="parameters">The set of query parameters.</param>
        public QueryModel(IEnumerable<FilterModel> filters, IEnumerable<ParameterModel> parameters)
        {
            Filters = new ReadOnlyCollection<FilterModel>(new List<FilterModel>(filters ?? Enumerable.Empty<FilterModel>()));
            Parameters = new ReadOnlyCollection<ParameterModel>(new List<ParameterModel>(parameters ?? Enumerable.Empty<ParameterModel>()));
        }

        /// <summary>
        /// Gets the collection of filters.
        /// </summary>
        public ReadOnlyCollection<FilterModel> Filters { get; private set; }

        /// <summary>
        /// Gets the collection of parameters.
        /// </summary>
        public ReadOnlyCollection<ParameterModel> Parameters { get; private set; }
    }
}