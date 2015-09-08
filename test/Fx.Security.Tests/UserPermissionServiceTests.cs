// <copyright company="Fresh Egg Limited" file="UserPermissionServiceTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Xunit;
    using Fx.TestHelpers;

    /// <summary>
    /// Provides tests for the <see cref="UserPermissionService"/> type.
    /// </summary>
    public class UserPermissionServiceTests : ServiceTestBase<UserPermissionService, SecurityDbContext, UserPermission>
    {
        [Fact]
        public void GetForUser_ReturnsValidUserPermissionsForUser()
        {
            // Arrange

            // Act
            var adminPermissions = Service.GetForUser(1 /* Fresh Egg Administrator */).ToList();
            var mattPermissions = Service.GetForUser(2 /* Matthew Abbott */).ToList();

            // Assert
            Assert.Equal(2, adminPermissions.Count);
            Assert.Contains(adminPermissions, up => up.PermissionCode == "Admin");
            Assert.Contains(adminPermissions, up => up.PermissionCode == "Owner");

            Assert.Equal(1, mattPermissions.Count);
            Assert.Contains(mattPermissions, up => up.PermissionCode == "Public");
        }

        /// <summary>
        /// Populates the context with test data.
        /// </summary>
        /// <param name="context">The database context.</param>
        public override void PopulateData(SecurityDbContext context)
        {
            var user1 = new User
            {
                Id = 1,
                Forename = "FreshEgg",
                Surname = "Administrator",
                Email = "admin@freshegg.com",
                Username = "admin"
            };

            var user2 = new User
            {
                Id = 2,
                Forename = "Matthew",
                Surname = "Abbott",
                Email = "matthew.abbott@freshegg.com",
                Username = "abbottm"
            };

            context.Users.AddRange(user1, user2);

            var permission1 = new UserPermission
            {
                Id = 1,
                PermissionCode = "Admin",
                UserId = 1
            };

            var permission2 = new UserPermission
            {
                Id = 2,
                PermissionCode = "Owner",
                UserId = 1
            };

            var permission3 = new UserPermission
            {
                Id = 3,
                PermissionCode = "Public",
                UserId = 2
            };

            context.UserPermissions.AddRange(permission1, permission2, permission3);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.UserPermissions)
                    .WithPermissions(SecurityPermissions.Users)
                    .Build()
            };
        }
    }
}
