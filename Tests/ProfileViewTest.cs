using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoldierInfoContext;
using System.Threading.Tasks;
using ValorVault.Controllers;
using ValorVault.Models;
using ValorVault.Services;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class ProfileViewControllerTests
    {
        [TestMethod]
        public async Task GetProfile_ReturnsNotFound_ForNonExistentProfile()
        {
            // Arrange
            var mockProfileService = new Mock<IProfileService>();
            mockProfileService.Setup(service => service.GetProfile(It.IsAny<int>()))
                .ReturnsAsync((SoldierInfo)null);
            var controller = new ProfileViewController(mockProfileService.Object);

            // Act
            var result = await controller.GetProfile(999); // Передаємо неіснуючий ID профіля

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProfile_ReturnsProfile_ForExistingProfile()
        {
            // Arrange
            var soldierInfo = new SoldierInfo { soldier_info_id = 4, soldier_name = "Maz Bist" };
            var mockProfileService = new Mock<IProfileService>();
            mockProfileService.Setup(service => service.GetProfile(4))
                .ReturnsAsync(soldierInfo);
            var controller = new ProfileViewController(mockProfileService.Object);

            // Act
            var result = await controller.GetProfile(4); // Передаємо ID існуючого профіля

            // Assert
            Assert.IsNotNull(result, "Result is null.");
            var profile = result.Value as SoldierInfo;
            Assert.IsNotNull(profile, "Profile is null.");
            Assert.AreEqual("Maz Bist", profile.soldier_name);
        }
    }
}
