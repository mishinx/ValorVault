using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ValorVault.Controllers;
using ValorVault.Models;
using System;
using SoldierInfoContext;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Tests
{
    [TestClass]
    public class SoldierInfoesControllerTests
    {
        private Mock<DbSet<SoldierInfo>> mockSet;
        private Mock<SoldierInfoDbContext> mockContext;
        private SoldierInfoesController controller;

        [TestInitialize]
        public void Setup()
        {
            IQueryable<SoldierInfo> soldierInfoData = new List<SoldierInfo>
            {
                new SoldierInfo { soldier_info_id = 1, soldier_name = "John Doe", call_sign = "Alpha", age = 30 }
            }.AsQueryable();

            mockSet = GetMockDbSet(soldierInfoData);
            mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(m => m.SoldierInfos).Returns(mockSet.Object);
            controller = new SoldierInfoesController(mockContext.Object);
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        [TestMethod]
        public async Task Create_Post_ValidData_ReturnsRedirectToIndexView()
        {
            var newSoldier = new ValorVault.Models.SoldierInfo
            {
                soldier_info_id = 2,
                soldier_name = "Test Test",
                call_sign = "Bravo",
                age = 54
            };

            controller.ModelState.Clear();

            // Act
            var result = await controller.Create(newSoldier);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<ValorVault.Models.SoldierInfo>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Edit_Post_ValidData_ReturnsRedirectToIndexView()
        {
            // Arrange
            var soldierId = 1;
            var updatedSoldier = new SoldierInfo
            {
                soldier_info_id = soldierId,
                soldier_name = "Updated Name",
                call_sign = "Updated Call Sign",
                age = 35
            };

            // Assuming FindAsync is used to retrieve the entity before updating
            mockSet.Setup(m => m.FindAsync(soldierId))
                   .ReturnsAsync(updatedSoldier);

            // Simulating that the entity is being tracked
            mockContext.Setup(m => m.Set<SoldierInfo>()).Returns(mockSet.Object);
            mockContext.Setup(m => m.Entry(It.IsAny<SoldierInfo>()))
                       .Returns((SoldierInfo si) =>
                       {
                           var entry = new Mock<EntityEntry<SoldierInfo>>();
                           entry.Setup(x => x.Entity).Returns(si);
                           entry.SetupProperty(x => x.State);  // Allows us to set the state
                           return entry.Object;
                       });

            // Act
            var result = await controller.Edit(soldierId, updatedSoldier);

            // Assert
            mockSet.Verify(m => m.Update(It.Is<SoldierInfo>(si => si.soldier_info_id == soldierId && si == updatedSoldier)), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }




    }
}
