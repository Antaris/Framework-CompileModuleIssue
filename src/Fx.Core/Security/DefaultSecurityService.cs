// <copyright company="Fresh Egg Limited" file="DefaultIdentityService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;

    /// <summary>
    /// Provides a base implementation of a security service.
    /// </summary>
    public class DefaultSecurityService : ISecurityService
    {
        private readonly IHost _host;

        /// <summary>
        /// Initialises a new instance of <see cref="DefaultSecurityService" />
        /// </summary>
        /// <param name="host">The host.</param>
        public DefaultSecurityService(IHost host)
        {
            _host = Ensure.ArgumentNotNull(host, nameof(host));
        }

        /// <inheritdoc />
        public ISecurityContext GetCurrentSecurityContext()
        {
            return new SecurityContext(Enumerable.Empty<Permission>(), Enumerable.Empty<DataSetPermission>(), _host.GetAvailableDataSets());
        }
    }
}