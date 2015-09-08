// <copyright company="Fresh Egg Limited" file="RoleServiceTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Xunit;
    using Fx.TestHelpers;


    /// <summary>
    /// Provides tests for the <see cref="RoleService"/> type.
    /// </summary>
    public class RoleServiceTests : ServiceTestBase<RoleService, SecurityDbContext, Role>
    {
        /// <summary>
        /// Populates the context with test data.
        /// </summary>
        /// <param name="context">The database context.</param>
        public override void PopulateData(SecurityDbContext context)
        {
            var role1 = new Role
            {
                Id = 1,
                Name = "Administrator"
            };

            var role2 = new Role
            {
                Id = 2,
                Name = "Content Editor"
            };

            var role3 = new Role
            {
                Id = 3,
                Name = "Content Publisher"
            };

            context.Roles.AddRange(role1, role2, role3);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.Roles)
                    .WithPermissions(SecurityPermissions.Roles)
                    .Build()
            };
        }
    }
}