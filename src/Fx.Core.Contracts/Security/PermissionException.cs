// <copyright company="Fresh Egg Limited" file="PermissionException.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents an exception thrown due to invalid permissions for an operation.
    /// </summary>
    public class PermissionException : Exception
    {
        private readonly IIdentity _identity;
        private readonly Permission _permission;
        private readonly DataSet _dataset;
        private readonly DataSetPermissionType _type;

        /// <summary>
        /// Initialises a new instance of <see cref="PermissionException" /> from the given identity and permission.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="permission">The permission.</param>
        public PermissionException(IIdentity identity, Permission permission)
            : base(CreateMessage(identity, permission))
        {
            _identity = identity;
            _permission = permission;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="PermissionException" /> from the given identity, dataset and dataset permission type.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="type">The dataset permission type.</param>
        public PermissionException(IIdentity identity, DataSet dataset, DataSetPermissionType type)
            : base(CreateMessage(identity, dataset, type))
        {
            _identity = identity;
            _dataset = dataset;
            _type = type;
        }

        private static string CreateMessage(IIdentity identity, Permission permission)
        {
            return $"{identity.Name} does not have the {permission.Name} permission.";
        }

        private static string CreateMessage(IIdentity identity, DataSet dataset, DataSetPermissionType type)
        {
            return $"{identity.Name} does not have the {type} permission to access the {dataset.Name} dataset.";
        }
    }
}
