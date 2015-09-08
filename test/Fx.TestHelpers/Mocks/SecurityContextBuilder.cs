// <copyright company="Fresh Egg Limited" file="TestSecurityContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fx.Security;

    /// <summary>
    /// Provides a test security context.
    /// </summary>
    public class SecurityContextBuilder
    {
        private List<Permission> _permissions = new List<Permission>();
        private List<DataSetPermission> _datasetPermissions = new List<DataSetPermission>();
        private List<DataSet> _datasets = new List<DataSet>();

        public SecurityContextBuilder WithPermissions(params Permission[] permissions)
        {
            Ensure.ArgumentNotNull(permissions, nameof(permissions));
            if (permissions.Length > 0)
            {
                foreach (var permission in permissions)
                {
                    if (!_permissions.Any(p => p.Equals(permission)))
                    {
                        _permissions.Add(permission);
                    }
                }
            }

            return this;
        }

        public SecurityContextBuilder WithDataSets(params DataSet[] datasets)
        {
            Ensure.ArgumentNotNull(datasets, nameof(datasets));
            if (datasets.Length > 0)
            {
                foreach (var dataset in datasets)
                {
                    if (!_datasets.Any(p => p.Equals(dataset)))
                    {
                        _datasets.Add(dataset);
                    }
                }
            }

            return this;
        }

        public SecurityContextBuilder WithDataSetPermission(DataSet dataset, DataSetPermissionType type)
        {
            Ensure.ArgumentNotNull(dataset, nameof(dataset));

            var existing = _datasetPermissions.SingleOrDefault(dp => dp.DataSet.Equals(dataset));
            if (existing == null)
            {
                existing = new DataSetPermission(dataset, false, false, false, false);
                _datasetPermissions.Add(existing);
            }

            WithDataSets(dataset);

            switch (type)
            {
                case DataSetPermissionType.Create:
                    existing.Create = true;
                    break;
                case DataSetPermissionType.Read:
                    existing.Read = true;
                    break;
                case DataSetPermissionType.Update:
                    existing.Update = true;
                    break;
                case DataSetPermissionType.Delete:
                    existing.Delete = true;
                    break;
            }

            return this;
        }

        public SecurityContext Build()
        {
            return new SecurityContext(_permissions, _datasetPermissions, _datasets);
        }
    }
}
