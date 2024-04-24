using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoldierInfoContext;
using System.Collections.Generic;
using System.Linq;
using ValorVault.Models;
using ValorVault.Services;

namespace ValorVault.Tests.Services
{
    [TestClass]
    public class UserModerationServiceTests
    {
        [TestMethod]
        public void DeleteUser_WhenUserExists_ShouldRemoveUserFromContext()
        {
            // Arrange
            var testData = new List<User>
            {
                new User { UserId = 1, username = "user1", email = "user1@example.com", user_password = "password1" },
                new User { UserId = 2, username = "user2", email = "user2@example.com", user_password = "password2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            // Setup Find method
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                   .Returns<object[]>(ids => testData.FirstOrDefault(u => u.UserId == (int)ids[0]));

            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(c => c.users).Returns(mockSet.Object);

            var service = new UserModerationService(mockContext.Object);

            // Act
            service.DeleteUser(1); // Припустимо, що ID користувача для видалення - 1

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }


        [TestMethod]
        public void DeleteUser_WhenUserDoesNotExist_ShouldNotCallSaveChanges()
        {
            // Arrange
            var testData = Enumerable.Empty<User>().AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(c => c.users).Returns(mockSet.Object);

            var service = new UserModerationService(mockContext.Object);

            // Act
            service.DeleteUser(1); // Припустимо, що користувача з ID 1 не існує

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Never);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}
