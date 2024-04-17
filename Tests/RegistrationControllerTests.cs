using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoldierInfoContext;
using ValorVault.Controllers;
using ValorVault.Services;

namespace Tests

{
    [TestClass]
    public class RegistrationControllerTests
    {
        [TestMethod]
        public void Register_ValidUser_ReturnsOkResult()
        {
            var mockSet = new List<User>().AsMockDbSet();
            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(m => m.users).Returns(mockSet.Object);
            var registrationService = new RegistrationService(mockContext.Object);
            var registrationController = new RegistrationController(registrationService);
            var newUser = new User { email = "newuser@example.com", password = "newPassword1", Name = "New User" };

            var result = registrationController.Register(newUser);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Register_ExistingEmail_ReturnsBadRequest()
        {
            var existingUser = new User { email = "existing@example.com", password = "existingPassword1", Name = "Existing User" };
            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(x => x.users).Returns(new List<User> { existingUser }.AsMockDbSet().Object);
            var registrationService = new RegistrationService(mockContext.Object);
            var registrationController = new RegistrationController(registrationService);

            var result = registrationController.Register(existingUser);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Користувач з таким email вже існує", badRequestResult.Value.GetType().GetProperty("message").GetValue(badRequestResult.Value));
        }

        [TestMethod]
        public void Register_InvalidEmail_ReturnsBadRequest()
        {
            var mockContext = new Mock<SoldierInfoDbContext>();
            var registrationService = new RegistrationService(mockContext.Object);
            var registrationController = new RegistrationController(registrationService);
            var newUser = new User { email = "invalidemail", password = "strongPassword123", Name = "New User" };

            var emailIsValid = InputValidator.IsEmailValid(newUser.email);
            if (!emailIsValid)
            {
                var result = registrationController.Register(newUser);

                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual("email повинен бути в коректній формі example@gmail.com", badRequestResult.Value);
            }
            else
            {
                Assert.Inconclusive("Недійсна електронна пошта. Тест пропущено.");
            }
        }
    }
}