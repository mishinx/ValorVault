using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using ValorVault.Controllers;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;
using ValorVault.Models;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private RegistrationController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new RegistrationController(_mockUserService.Object);
        }

        [TestMethod]
        public async Task Register_WithValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var model = new RegisterUserDto { Username = "testuser", Email = "test@example.com", Password = "Password123", PasswordRepeat = "Password123" };
            _mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ReturnsAsync(new User());

            // Act
            var result = await _controller.Register(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [TestMethod]
        public void Register_WithInvalidModel_ThrowsArgumentException()
        {
            // Arrange
            var model = new RegisterUserDto { Username = "testuser", Email = "test@example.com", Password = "Password123", PasswordRepeat = "DifferentPassword" };

            // Act & Assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _controller.Register(model));
        }

        [TestMethod]
        public void Register_WithExistingEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            var model = new RegisterUserDto { Username = "testuser", Email = "test@example.com", Password = "Password123", PasswordRepeat = "Password123" };
            _mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ThrowsAsync(new InvalidOperationException("User with this email already exists."));

            // Act & Assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _controller.Register(model));
        }

        [TestMethod]
        public void Register_WithExistingUsername_ThrowsInvalidOperationException()
        {
            // Arrange
            var model = new RegisterUserDto { Username = "testuser", Email = "test@example.com", Password = "Password123", PasswordRepeat = "Password123" };
            _mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ThrowsAsync(new InvalidOperationException("User with this username already exists."));

            // Act & Assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _controller.Register(model));
        }

        [TestMethod]
        public void Register_WithFailedUserCreation_ThrowsInvalidOperationException()
        {
            // Arrange
            var model = new RegisterUserDto { Username = "testuser", Email = "test@example.com", Password = "Password123", PasswordRepeat = "Password123" };
            _mockUserService.Setup(x => x.CreateUser(It.IsAny<RegisterUserDto>())).ThrowsAsync(new InvalidOperationException("Failed to create user."));

            // Act & Assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _controller.Register(model));
        }
    }
}
