// <copyright company="Fresh Egg Limited" file="RolePermissionServiceTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Xunit;
    using Fx.TestHelpers;

    /// <summary>
    /// Provides tests for the <see cref="RolePermissionService"/> type.
    /// </summary>
    public class RolePermissionServiceTests : ServiceTestBase<RolePermissionService, SecurityDbContext, RolePermission>
    {
        [Fact]
        public void GetForRole_ReturnsValidRolePermissionsForRole()
        {
            // Arrange

            // Act
            var adminPermissions = Service.GetForRole(1 /* Administrator */).ToList();
            var editorPermissions = Service.GetForRole(2 /* Content Editor */).ToList();
            var publisherPermissions = Service.GetForRole(3 /* Content Publisher */).ToList();

            // Assert
            Assert.Equal(2, adminPermissions.Count);
            Assert.Contains(adminPermissions, rp => rp.PermissionCode == "Admin");
            Assert.Contains(adminPermissions, rp => rp.PermissionCode == "Owner");

            Assert.Equal(1, editorPermissions.Count);
            Assert.Contains(editorPermissions, rp => rp.PermissionCode == "Page");

            Assert.Equal(1, publisherPermissions.Count);
            Assert.Contains(publisherPermissions, rp => rp.PermissionCode == "Page");
        }

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

            var permission1 = new RolePermission
            {
                Id = 1,
                PermissionCode = "Admin",
                RoleId = 1
            };

            var permission2 = new RolePermission
            {
                Id = 2,
                PermissionCode = "Owner",
                RoleId = 1
            };

            var permission3 = new RolePermission
            {
                Id = 3,
                PermissionCode = "Page",
                RoleId = 2
            };

            var permission4 = new RolePermission
            {
                Id = 4,
                PermissionCode = "Page",
                RoleId = 3
            };

            context.RolePermissions.AddRange(permission1, permission2, permission3, permission4);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.RolePermissions)
                    .WithPermissions(SecurityPermissions.Roles)
                    .Build()
            };
        }
    }
}
