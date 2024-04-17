using Microsoft.EntityFrameworkCore;
using Moq;
using SoldierInfoContext;
using ValorVault.Services;

namespace Tests
{
    public static class DbSetExtensions
    {
        public static Mock<DbSet<T>> AsMockDbSet<T>(this IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockSet;
        }
    }

    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUser()
        {
            var testUsers = new List<User>
            {
                new User { email = "test@example.com", password = "testPassword", Name = "Test User" }
            };
            var mockDbSet = testUsers.AsMockDbSet();

            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var authenticationService = new AuthenticationService(mockContext.Object);

            var authenticatedUser = authenticationService.Authenticate("test@example.com", "testPassword");

            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual("test@example.com", authenticatedUser.email);
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_ReturnsNull()
        {
            var testUsers = new List<User>
            {
                new User { email = "test@example.com", password = "testPassword", Name = "Test User" }
            };
            var mockDbSet = testUsers.AsMockDbSet();

            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var authenticationService = new AuthenticationService(mockContext.Object);

            var authenticatedUser = authenticationService.Authenticate("test@example.com", "wrongPassword");

            Assert.IsNull(authenticatedUser);
        }

        [TestMethod]
        public void Authenticate_EmailNotFound_ReturnsNull()
        {
            var testUsers = new List<User>
                    {
                        new User { email = "test@example.com", password = "testPassword", Name = "Test User" }
                    };
            var mockDbSet = testUsers.AsMockDbSet();
            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var authenticationService = new AuthenticationService(mockContext.Object);

            var authenticatedUser = authenticationService.Authenticate("nonexistent@example.com", "testPassword");

            Assert.IsNull(authenticatedUser);
        }
    }
}