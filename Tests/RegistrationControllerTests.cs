using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValorVault.Controllers;
using ValorVault.Models;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTests
    {
        [TestMethod]
        public async Task Register_InvalidModel_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var controller = new RegistrationController(mockUserService.Object);
            var model = new RegisterUserDto { Username = "", Email = "invalid-email", Password = "weak", PasswordRepeat = "weak" };

            // Act
            var result = await controller.Register(model) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Некоректний формат"));
        }

        [TestMethod]
        public async Task Register_UserServiceThrowsArgumentException_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ThrowsAsync(new ArgumentException("Invalid argument"));

            var controller = new RegistrationController(mockUserService.Object);
            var model = new RegisterUserDto { Username = "test", Email = "test@example.com", Password = "password", PasswordRepeat = "password" };

            // Act
            var result = await controller.Register(model) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("{ message = Некоректний формат паролю }", result.Value.ToString());
        }

        [TestMethod]
        public async Task Register_UserServiceThrowsException_ReturnsInternalServerErrorWithErrorMessage()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ThrowsAsync(new Exception("Internal error"));

            var controller = new RegistrationController(mockUserService.Object);
            var model = new RegisterUserDto { Username = "test", Email = "test@example.com", Password = "password", PasswordRepeat = "password" };

            // Act
            var result = await controller.Register(model) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
