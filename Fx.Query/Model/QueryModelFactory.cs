// <copyright company="Fresh Egg Limited" file="QueryModelFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provides factory methods for creating <see cref="QueryModel"/> instances.
    /// </summary>
    public static class QueryModelFactory
    {
        private static readonly string[] LogicValues = Enum.GetNames(typeof(FilterGroupLogic)).ToArray();
        private static readonly string[] OperatorValues = Enum.GetNames(typeof(FilterOperator)).ToArray();
        private static readonly ConcurrentDictionary<int, PropertyModel> _propertyCache = new ConcurrentDictionary<int, PropertyModel>();

        /// <summary>
        /// Creates a <see cref="QueryModel"/> from the given JSON string.
        /// </summary>
        /// <param name="json">The JSON string representing the query.</param>
        /// <returns>The <see cref="QueryModel"/> instance.</returns>
        public static QueryModel<T> CreateModel<T>(string json)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(json, nameof(json));

            return CreateModel<T>(JObject.Parse(json));
        }

        /// <summary>
        /// Creates a <see cref="QueryModel"/> from the given <see cref="JObject"/
        /// </summary>
        /// <param name="root">The <see cref="JObject"/> representing the query.</param>
        /// <returns>The <see cref="QueryModel"/> instance</returns>
        public static QueryModel<T> CreateModel<T>(JObject root)
        {
            var filters = CreateFilterModels<T>(root).ToList();
            if (filters.Count > 1)
            {
                var group = new FilterGroupModel(filters, FilterGroupLogic.And);
                filters = new List<FilterModel>() { group };
            }           

            return new QueryModel<T>(filters, null);
        }

        /// <summary>
        /// Creates a set of <see cref="FilterModel"/> represented by the given root object.
        /// </summary>
        /// <param name="root">The root object.</param>
        /// <returns>The set of filter models.</returns>
        public static IEnumerable<FilterModel> CreateFilterModels<T>(JObject root)
        {
            Ensure.ArgumentNotNull(root, nameof(root));

            var prop = root.Property(JsonQueryStructureConstants.FiltersProperty);
            if (prop != null && prop.Value != null)
            {
                var array = prop.Value as JArray;
                var obj = prop.Value as JObject;

                if (array != null)
                {
                    return CreateFilterModels<T>(array);
                }
                else if (obj != null)
                {
                    return new[] { CreateFilterModel<T>(obj) };
                }
            }

            return Enumerable.Empty<FilterModel>();
        }

        /// <summary>
        /// Creates a set of filters provided by the given array.
        /// </summary>
        /// <param name="array">The array of items.</param>
        /// <returns>The set of filter models.</returns>
        public static IEnumerable<FilterModel> CreateFilterModels<T>(JArray array)
        {
            Ensure.ArgumentNotNull(array, nameof(array));

            var list = new List<FilterModel>();

            for (int i = 0; i < array.Count; i++)
            {
                var item = array[i];
                var arr = item as JArray;
                var obj = item as JObject;

                if (arr != null)
                {
                    var filters = CreateFilterModels<T>(arr);
                    list.Add(new FilterGroupModel(filters, FilterGroupLogic.And));
                }
                else if (obj != null)
                {
                    list.Add(CreateFilterModel<T>(obj));
                }
            }

            return list.AsEnumerable();
        }

        /// <summary>
        /// Creates a filter model for the given JSON object.
        /// </summary>
        /// <param name="obj">The JSON object representing a filter.</param>
        /// <returns>The filter model.</returns>
        public static FilterModel CreateFilterModel<T>(JObject obj)
        {
            Ensure.ArgumentNotNull(obj, nameof(obj));

            var logic = LogicValues.Select(v => obj.Property(v)).FirstOrDefault(p => p != null && p.Value != null);
            if (logic != null)
            {
                var filters = new List<FilterModel>();

                var array = logic.Value as JArray;
                var obj1 = logic.Value as JObject;

                if (array != null)
                {
                    filters.AddRange(CreateFilterModels<T>(array));
                }
                else
                {
                    filters.Add(CreateFilterModel<T>(obj1));
                }

                return new FilterGroupModel(filters, (FilterGroupLogic)Enum.Parse(typeof(FilterGroupLogic), logic.Name, ignoreCase: true));
            }
            else
            {
                return CreateFilterModelItem<T>(obj);
            }
        }

        /// <summary>
        /// Creates a filter model from the given JSON object. This model will be a filter, not a filter group.
        /// </summary>
        /// <param name="obj">The JSON object representing a filter.</param>
        /// <returns>The filter model.</returns>
        public static FilterModel CreateFilterModelItem<T>(JObject obj)
        {
            /**
            var type = typeof(T);
            var objectType = typeof(object);

            var properties = Properties(type, obj);
            var op = Operator(obj);

            var propertyType = ValidateProperties(properties);
            var elementType = propertyType.ResovleElementType();

            if (IsSet(type, propertyType))
            {

            }

            var targetType = (elementType == objectType) ? null : elementType) ?? propertyType;

            **/
            throw new Exception("");
        }

        /// <summary>
        /// Creates a property model
        /// </summary>
        /// <param name="type">The node type.</param>
        /// <param name="path">The property path.</param>
        /// <returns>The property model.</returns>
        public static PropertyModel CreatePropertyModel(Type type, string path)
        {
            Ensure.ArgumentNotNull(type, nameof(type));
            Ensure.ArgumentNotNullOrWhiteSpace(path, nameof(path));

            int hash = 17;
            unchecked
            {
                hash = hash * 23 + type.GetHashCode();
                hash = hash * 23 + path.GetHashCode();
            }

            return _propertyCache.GetOrAdd(hash, h => CreatePropertyModelCore(type, path));
        }

        /// <summary>
        /// Creates a property model
        /// </summary>
        /// <param name="type">The node type.</param>
        /// <param name="path">The property path.</param>
        /// <param name="root">The root path.</param>
        /// <returns>The property model.</returns>
        private static PropertyModel CreatePropertyModelCore(Type type, string path, string root = null)
        {
            if (string.IsNullOrWhiteSpace(root))
            {
                root = path;
            }

            int index = path.IndexOfAny(new[] {  '.', '[' });
            string prop = (index > -1) ? path.Substring(0, index) : path;

            var property = type.GetProperty(prop);
            if (property == null)
            {
                return null;
            }

            string remaining = path.Substring(prop.Length);
            if (remaining.StartsWith("["))
            {
                remaining = remaining.Substring(0, remaining.IndexOf(']') + 1);
            }
            else if (remaining.StartsWith("."))
            {
                remaining = remaining.Substring(1);
            }

            if (!string.IsNullOrWhiteSpace(remaining))
            {
                return CreatePropertyModelCore(property.PropertyType, remaining, root);
            }

            return new PropertyModel(root, property.PropertyType);
        }

        /// <summary>
        /// Gets the node of the given type from the given token.
        /// </summary>
        /// <typeparam name="J">The JSON node type.</typeparam>
        /// <param name="node">The node instance.</param>
        /// <param name="name">The property name.</param>
        /// <returns>The JSON subnode.</returns>
        private static J Node<J>(JObject node, string name) where J : JToken
        {
            var prop = node.Property(name);
            if (prop == null || prop.Value == null || !(prop.Value is J))
            {
                return null;
            }

            return (J)prop.Value;
        }

        /// <summary>
        /// Returns the set of properties
        /// </summary>
        /// <param name="root">The root type</param>
        /// <param name="node">The JSON node.</param>
        /// <returns>The set of property models.</returns>
        private static IEnumerable<PropertyModel> Properties(Type root, JObject node)
        {
            var array = Node<JArray>(node, JsonQueryStructureConstants.FieldsProperty);
            if (array == null || array.Count == 0)
            {
                var single = Node<JValue>(node, JsonQueryStructureConstants.FieldsProperty);
                if (single != null)
                {
                    yield return CreatePropertyModel(root, single.Value<string>());
                }
                else
                {
                    yield return new PropertyModel("", root);
                }
            }
            else
            {
                for (int i = 0; i < array.Count; i++)
                {
                    string path = array[i].Value<string>();
                    yield return CreatePropertyModel(root, path);
                }
            }
        }
    }
}
