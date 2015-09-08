// <copyright company="Fresh Egg Limited" file="SecurityContextTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System;
    using Xunit;
    using Fx.TestHelpers;

    /// <summary>
    /// Provides tests for the <see cref="SecurityContext"/> type.
    /// </summary>
    public class SecurityContextTests
    {
        [Fact]
        public void Constructor_ThrowsException_WhenNullPermissionsSetProvided()
        {
            // Arrange

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new SecurityContext(null, null, null));

            // Assert
            Assert.Equal("permissions", exception.ParamName);
        }

        [Fact]
        public void Constructor_ThrowsException_WhenNullDataSetPermissionsSetProvided()
        {
            // Arrange

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new SecurityContext(new Permission[0], null, null));

            // Assert
            Assert.Equal("datasetPermissions", exception.ParamName);
        }

        [Fact]
        public void Constructor_ThrowsException_WhenNullDataSetSetProvided()
        {
            // Arrange

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new SecurityContext(new Permission[0], new DataSetPermission[0], null));

            // Assert
            Assert.Equal("datasets", exception.ParamName);
        }

        [Fact]
        public void HasPermission_ThrowsException_WhenNullPermissionProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => context.HasPermission((Permission)null));

            // Assert
            Assert.Equal("permission", exception.ParamName);
        }

        [Fact]
        public void HasPermission_ReturnsTrue_WithValidPermission()
        {
            // Arrange
            var context = new SecurityContextBuilder().WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasPermission(CorePermissions.Admin);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasPermission_ReturnsFalse_WithInvalidPermission()
        {
            // Arrange
            var context = new SecurityContextBuilder().WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasPermission(CorePermissions.Owner);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ThrowsException_WhenNullPermissionCodeProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasPermission((string)null));

            // Assert
            Assert.Equal("code", exception.ParamName);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ThrowsException_WhenEmptyPermissionCodeProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasPermission(""));

            // Assert
            Assert.Equal("code", exception.ParamName);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ThrowsException_WhenWhiteSpacePermissionCodeProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasPermission("  "));

            // Assert
            Assert.Equal("code", exception.ParamName);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ReturnsTrue_ForValidPermissionCode()
        {
            // Assert
            var context = new SecurityContextBuilder().WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasPermission("Admin");

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ReturnsTrue_ForValidPermissionCode_UsingMixedCaseCode()
        {
            // Assert
            var context = new SecurityContextBuilder().WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasPermission("adMIn");

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasPermission_UsingStringPermission_ReturnsFalse_ForInvalidPermissionCode()
        {
            // Assert
            var context = new SecurityContextBuilder().WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasPermission("Yada");

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ThrowsException_WhenNullDataSetProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => context.HasDataSetPermission((DataSet)null, DataSetPermissionType.Create));

            // Assert
            Assert.Equal("dataset", exception.ParamName);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsTrue_ForValidCreatePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Create);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsFalse_ForInvalidCreatePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Read).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Create);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsTrue_ForValidReadPermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Read).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Read);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsFalse_ForInvalidReadPermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Read);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsTrue_ForValidUpdatePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Update).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Update);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsFalse_ForInvalidUpdatePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Update);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsTrue_ForValidDeletePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Delete).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Delete);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsFalse_ForInvalidDeletePermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Delete);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsFalse_ForInvalidPermission_AndUnknownDataSet()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var otherDataSet = new DataSet("Other");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(otherDataSet, DataSetPermissionType.Read);

            // Assert
            Assert.False(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_ReturnsTrue_ForImpliedPermissionFromDataSet()
        {
            // Arrange
            var dataset = new DataSet("Test", impliedBy: CorePermissions.Admin);
            var context = new SecurityContextBuilder().WithDataSets(dataset).WithPermissions(CorePermissions.Admin).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission(dataset, DataSetPermissionType.Create);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ThrowsException_WhenNullDataSetNameProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasDataSetPermission((string)null, DataSetPermissionType.Create));

            // Assert
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ThrowsException_WhenEmptyDataSetNameProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasDataSetPermission("", DataSetPermissionType.Create));

            // Assert
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ThrowsException_WhenWhiteSpaceDataSetNameProvided()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => context.HasDataSetPermission("  ", DataSetPermissionType.Create));

            // Assert
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ReturnsTrue_ForValidDataSetPermission()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission("Test", DataSetPermissionType.Create);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ReturnsTrue_ForValidDataSetPermission_UsingMixedCaseName()
        {
            // Arrange
            var dataset = new DataSet("Test");
            var context = new SecurityContextBuilder().WithDataSetPermission(dataset, DataSetPermissionType.Create).Build();

            // Act
            bool hasPermission = context.HasDataSetPermission("teST", DataSetPermissionType.Create);

            // Assert
            Assert.True(hasPermission);
        }

        [Fact]
        public void HasDataSetPermission_UsingStringDataSet_ReturnsFalse_ForInvalidDataSet()
        {
            // Arrange
            var context = new SecurityContextBuilder().Build();

            // Act
            bool hasPermission = context.HasDataSetPermission("Test", DataSetPermissionType.Create);

            // Assert
            Assert.False(hasPermission);
        }
    }
}