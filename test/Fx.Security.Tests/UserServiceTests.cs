// <copyright company="Fresh Egg Limited" file="UserServiceTests.cs">
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
    /// Provides tests for the <see cref="UserService"/> type.
    /// </summary>
    public class UserServiceTests : ServiceTestBase<UserService, SecurityDbContext, User>
    {
        [Fact]
        public void ExistsByUsername_ReturnsTrueForValidUser()
        {
            // Arrange

            // Act
            bool exists = Service.Exists("admin");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void ExistsByUsername_ReturnsFalseForInvalidUser()
        {
            // Arrange

            // Act
            bool exists = Service.Exists("invaliduser");

            // Assert
            Assert.True(!exists);
        }

        [Fact]
        public void GetInRoles_ReturnsValidUsersForRole()
        {
            // Arrange

            // Act
            var admins = Service.GetAllInRole(1 /* Administrator */).ToList();
            var editors = Service.GetAllInRole(2 /* Content Editor */).ToList();
            var publishers = Service.GetAllInRole(3 /* Content Publisher */).ToList();

            // Assert
            Assert.Equal(1, admins.Count);
            Assert.Contains(admins, u => u.Id == 1 /* Fresh Egg Administrator */);

            Assert.Equal(1, editors.Count);
            Assert.Contains(editors, u => u.Id == 2 /* Matthew Abbott */);

            Assert.Equal(2, publishers.Count);
            Assert.Contains(publishers, u => u.Id == 1 /* Fresh Egg Administrator */);
            Assert.Contains(publishers, u => u.Id == 2 /* Matthew Abbott */);
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

            var user1 = new User
            {
                Id = 1,
                Forename = "FreshEgg",
                Surname = "Administrator",
                Email = "admin@freshegg.com",
                Username = "admin",
                Roles = new List<Role>() {  role1, role3 }
            };

            var user2 = new User
            {
                Id = 2,
                Forename = "Matthew",
                Surname = "Abbott",
                Email = "matthew.abbott@freshegg.com",
                Username = "abbottm",
                Roles = new List<Role>() { role2, role3 }
            };

            context.Users.AddRange(user1, user2);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.Users)
                    .WithPermissions(SecurityPermissions.Users)
                    .Build()
            };
        }
    }
}