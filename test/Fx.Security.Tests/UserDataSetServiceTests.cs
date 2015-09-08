// <copyright company="Fresh Egg Limited" file="UserDataSetServiceTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System.Linq;
    using Microsoft.Framework.DependencyInjection;
    using Xunit;
    using Fx.TestHelpers;

    /// <summary>
    /// Provides tests for the <see cref="UserDataSetService"/> type.
    /// </summary>
    public class UserDataSetServiceTests : ServiceTestBase<UserDataSetService, SecurityDbContext, UserDataSet>
    {
        [Fact]
        public void GetForUser_ReturnsValidUserDataSetsForUser()
        {
            // Arrange

            // Act
            var adminPermissions = Service.GetForUser(1 /* Fresh Egg Administrator */).ToList();
            var mattPermissions = Service.GetForUser(2 /* Matthew Abbott */).ToList();

            // Assert
            Assert.Equal(2, adminPermissions.Count);
            Assert.Contains(adminPermissions, ud => ud.DataSetName == "User");
            Assert.Contains(adminPermissions, ud => ud.DataSetName == "Role");

            Assert.Equal(1, mattPermissions.Count);
            Assert.Contains(mattPermissions, ud => ud.DataSetName == "Page");
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

            var dataset1 = new UserDataSet
            {
                Id = 1,
                DataSetName = "User",
                UserId = 1
            };

            var dataset2 = new UserDataSet
            {
                Id = 2,
                DataSetName = "Role",
                UserId = 1
            };

            var dataset3 = new UserDataSet
            {
                Id = 3,
                DataSetName = "Page",
                UserId = 2
            };

            context.UserDataSets.AddRange(dataset1, dataset2, dataset3);

            context.SaveChanges();
        }

        public override IWorkContext CreateWorkContext()
        {
            return new TestWorkContext()
            {
                SecurityContext = new SecurityContextBuilder()
                    .WithDataSets(SecurityDataSets.UserDataSets)
                    .WithPermissions(SecurityPermissions.Users)
                    .Build()
            };
        }
    }
}
