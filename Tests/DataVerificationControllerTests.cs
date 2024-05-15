using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValorVault.Controllers;
using ValorVault.Services;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class DataVerificationControllerTests
    {
        [TestMethod]
        public void MarkAsVerified_ReturnsOkResult_WhenServiceSucceeds()
        {
            // Arrange
            var soldierInfoId = 1;

            var mockService = new Mock<IDataVerificationService>();
            mockService.Setup(s => s.MarkSoldierInfoAsVerified(soldierInfoId));

            var controller = new DataVerificationController(mockService.Object);

            // Act
            var result = controller.MarkAsVerified(soldierInfoId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Анкета успішно підтверджена.", (result as OkObjectResult).Value);
        }

        [TestMethod]
        public void MarkAsRejected_ReturnsOkResult_WhenServiceSucceeds()
        {
            // Arrange
            var soldierInfoId = 1;

            var mockService = new Mock<IDataVerificationService>();
            mockService.Setup(s => s.MarkSoldierInfoAsRejected(soldierInfoId));

            var controller = new DataVerificationController(mockService.Object);

            // Act
            var result = controller.MarkAsRejected(soldierInfoId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Анкета успішно відхилена.", (result as OkObjectResult).Value);
        }
    }
}