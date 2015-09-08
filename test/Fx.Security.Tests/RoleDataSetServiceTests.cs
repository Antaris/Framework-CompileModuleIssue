// <copyright company="Fresh Egg Limited" file="RoleDataSetServiceTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Xunit;
    using Fx.TestHelpers;

    /// <summary>
    /// Provides tests for the <see cref="RoleDataSetService"/> type.
    /// </summary>
    public class RoleDataSetServiceTests : ServiceTestBase<RoleDataSetService, SecurityDbContext, RoleDataSet>
    {
        [Fact]
        public void GetForRole_ReturnsValidRoleDataSetsForRole()
        {
            // Arrange

            // Act
            var adminPermissions = Service.GetForRole(1 /* Administrator */).ToList();
            var editorPermissions = Service.GetForRole(2 /* Content Editor */).ToList();

            // Assert
            Assert.Equal(2, adminPermissions.Count);
            Assert.Contains(adminPermissions, rd => rd.DataSetName == "User");
            Assert.Contains(adminPermissions, rd => rd.DataSetName == "Role");

            Assert.Equal(1, editorPermissions.Count);
            Assert.Contains(editorPermissions, rd => rd.DataSetName == "Page");
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

            context.Roles.AddRange(role1, role2);

            var dataset1 = new RoleDataSet
            {
                Id = 1,
                DataSetName = "User",
                RoleId = 1
            };

            var dataset2 = new RoleDataSet
            {
                Id = 2,
                DataSetName = "Role",
                RoleId = 1
            };

            var dataset3 = new RoleDataSet
            {
                Id = 3,
                DataSetName = "Page",
                RoleId = 2
            };

            context.RoleDataSets.AddRange(dataset1, dataset2, dataset3);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.RoleDataSets)
                    .WithPermissions(SecurityPermissions.Roles)
                    .Build()
            };
        }
    }
}
