using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValorVault.Controllers;
using ValorVault.Services;
using Microsoft.AspNetCore.Mvc;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class TelegramControllerTests
    {
        [TestMethod]
        public void ContactAdministrator_ReturnsRedirectToTelegram()
        {
            // Arrange
            var mockTelegramService = new Mock<ITelegramService>();
            mockTelegramService.Setup(x => x.GetTelegramAccountUrl()).Returns("https://t.me/BT_Git_Support");

            var controller = new TelegramController(mockTelegramService.Object);

            // Act
            var result = controller.ContactAdministrator() as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("https://t.me/BT_Git_Support", result.Url);
        }
    }
}
