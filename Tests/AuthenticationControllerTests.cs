using Microsoft.AspNetCore.Mvc;
using Moq;
using SoldierInfoContext;
using ValorVault.Controllers;
using ValorVault.Services;

namespace Tests
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUser()
        {
            var mockContext = new Mock<SoldierInfoDbContext>();
            var testUsers = new List<User>
            {
                new User { email = "test@example.com", password = "testPassword", Name = "Test User" }
            };
            var mockDbSet = testUsers.AsMockDbSet();
            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var authenticationService = new AuthenticationService(mockContext.Object);
            var controller = new AuthenticationController(authenticationService);

            var result = controller.Authenticate(new UserBase { email = "test@example.com", password = "testPassword" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual("test@example.com", ((User)okResult.Value).email);
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_ReturnsBadRequest()
        {
            var mockContext = new Mock<SoldierInfoDbContext>();
            var testUsers = new List<User>
            {
                new User { email = "test@example.com", password = "testPassword", Name = "Test User" }
            };
            var mockDbSet = testUsers.AsMockDbSet();
            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var authenticationService = new AuthenticationService(mockContext.Object);
            var controller = new AuthenticationController(authenticationService);

            var result = controller.Authenticate(new UserBase { email = "test@example.com", password = "wrongPassword" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is BadRequestObjectResult);
        }
    }
}
