// <copyright company="Fresh Egg Limited" file="SecurityContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a security context.
    /// </summary>
    public class SecurityContext : ISecurityContext
    {
        private readonly List<DataSetPermission> _datasetPermissions;
        private readonly List<Permission> _permissions;
        private readonly IEnumerable<DataSet> _datasets;

        /// <summary>
        /// Initialises a new instance of <see cref="SecurityContext" />.
        /// </summary>
        /// <param name="permissions">The set of applied permissions.</param>
        /// <param name="datasetPermissions">The set of applied dataset permissions.</param>
        /// <param name="datasets">The set of datasets available to the system.</param>
        public SecurityContext(IEnumerable<Permission> permissions, IEnumerable<DataSetPermission> datasetPermissions, IEnumerable<DataSet> datasets)
        {
            _permissions = Ensure.ArgumentNotNull(permissions, nameof(permissions)).ToList();
            _datasetPermissions = Ensure.ArgumentNotNull(datasetPermissions, nameof(datasetPermissions)).ToList();
            _datasets = Ensure.ArgumentNotNull(datasets, nameof(datasets));
        }

        /// <inheritdoc />
        public IEnumerable<DataSetPermission> AppliedDataSetPermissions { get { return _datasetPermissions.AsEnumerable(); } }

        /// <inheritdoc />
        public IEnumerable<Permission> AppliedPermissions {  get { return _permissions.AsEnumerable(); } }

        /// <inheritdoc />
        public bool HasDataSetPermission(DataSet dataset, DataSetPermissionType type)
        {
            Ensure.ArgumentNotNull(dataset, nameof(dataset));

            // Get the dataset permission.
            var perm = _datasetPermissions.SingleOrDefault(dsp => dsp.DataSet == dataset);
            if (perm == null)
            {
                // This permission doesn't exist, so does the actual dataset?
                if (_datasets.Any(ds => ds == dataset))
                {
                    // Do I have any permissions which imply this dataseT?
                    if (dataset.ImpliedBy != null && dataset.ImpliedBy.Any(p => HasPermission(p)))
                    {
                        // The permission implies the dataset permission for all operations.
                        return true;
                    }
                }

                return false;
            }

            switch (type)
            {
                case DataSetPermissionType.Create:
                    return perm.Create;
                case DataSetPermissionType.Read:
                    return perm.Read;
                case DataSetPermissionType.Update:
                    return perm.Update;
                case DataSetPermissionType.Delete:
                    return perm.Delete;
            }

            return false;
        }

        /// <inheritdoc />
        public bool HasDataSetPermission(string name, DataSetPermissionType type)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            var dataset = _datasets.SingleOrDefault(ds => string.Equals(name, ds.Name, StringComparison.InvariantCultureIgnoreCase));
            if (dataset == null)
            {
                // Dataset does not exist.
                return false;
            }

            return HasDataSetPermission(dataset, type);
        }

        /// <inheritdoc />
        public bool HasPermission(Permission permission)
        {
            Ensure.ArgumentNotNull(permission, nameof(permission));

            return _permissions.Any(p => p == permission);
        }

        /// <inheritdoc />
        public bool HasPermission(string code)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(code, nameof(code));

            return _permissions.Any(p => string.Equals(code, p.Code, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}