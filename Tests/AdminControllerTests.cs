using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValorVault.Controllers;
using ValorVault.Services;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void DeleteUser_ReturnsOkResult_WhenUserIsDeleted()
        {
            // Arrange
            var userId = 1; // Припустимо, що ID користувача - 1

            var mockService = new Mock<IUserModerationService>();
            mockService.Setup(s => s.DeleteUser(userId)).Verifiable(); // Налаштовуємо макет для перевірки виклику методу DeleteUser

            var controller = new AdminController(mockService.Object);

            // Act
            var result = controller.DeleteUser(userId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Користувач успішно видалений.", result.Value);
            mockService.Verify(s => s.DeleteUser(userId), Times.Once); // Перевіряємо, що метод DeleteUser був викликаний один раз з переданим userId
        }
    }
}
