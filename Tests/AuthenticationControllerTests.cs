using System;
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
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public void Authenticate_ValidUser_RedirectsToHomeIndex()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.SignInUser(It.IsAny<LoginUserDto>())).ReturnsAsync(true);

            var controller = new AuthenticationController(mockUserService.Object);
            var user = new User { email = "test@example.com", user_password = "password" };

            // Act
            var result = controller.Authenticate(user) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home", result.ControllerName);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void Authenticate_InvalidUser_ReturnsLoginViewWithError()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.SignInUser(It.IsAny<LoginUserDto>())).ReturnsAsync(false);

            var controller = new AuthenticationController(mockUserService.Object);
            var user = new User { email = "test@example.com", user_password = "password" };

            // Act
            var result = controller.Authenticate(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
            Assert.IsNotNull(result.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public void Authenticate_ServiceThrowsException_ReturnsLoginViewWithError()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.SignInUser(It.IsAny<LoginUserDto>())).Throws(new Exception("Error"));

            var controller = new AuthenticationController(mockUserService.Object);
            var user = new User { email = "test@example.com", user_password = "password" };

            // Act
            var result = controller.Authenticate(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
            Assert.IsNotNull(result.ViewData["ErrorMessage"]);
        }
    }
}
