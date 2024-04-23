using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoldierInfoContext;
using System.Collections.Generic;
using System.Linq;
using ValorVault.Models;
using ValorVault.Services;

namespace Tests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private Mock<DbSet<User>> mockSet;
        private Mock<SoldierInfoDbContext> mockContext;
        private RegistrationService service;

        [TestInitialize]
        public void Setup()
        {
            var users = new List<User>
            {
                new User { email = "existing@example.com", user_password = "existingPassword", username = "Existing User" }
            };
            mockSet = CreateMockSet(users);

            var options = new DbContextOptionsBuilder<SoldierInfoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            mockContext = new Mock<SoldierInfoDbContext>(options);
            mockContext.Setup(m => m.users).Returns(mockSet.Object);
            service = new RegistrationService(mockContext.Object);
        }

        private Mock<DbSet<User>> CreateMockSet(IEnumerable<User> data)
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockSet;
        }

        [TestMethod]
        public void Register_NewUser_ReturnsUser()
        {
            // Arrange
            var newUser = new User { email = "new@example.com", user_password = "ValidPassword123", username = "New User" };
            var userList = new List<User>();
            mockSet.Setup(m => m.Add(It.IsAny<User>()))
                .Callback<User>(user => userList.Add(user));
            mockSet.As<IEnumerable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            // Act
            var result = service.Register(newUser.email, newUser.user_password, newUser.username);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newUser.email, result.email);
            Assert.AreEqual(newUser.user_password, result.user_password);
            Assert.AreEqual(newUser.username, result.username);
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Register_ExistingEmail_ReturnsNull()
        {
            // Arrange
            var existingEmail = "existing@example.com";
            var existingUser = new User { email = existingEmail, user_password = "existingPassword", username = "Existing User" };
            var userList = new List<User> { existingUser };
            mockSet.As<IEnumerable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            // Act
            var result = service.Register(existingEmail, "password", "Name");

            // Assert
            Assert.IsNull(result);
        }
    }
}
