using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValorVault.Models;
using ValorVault.Services;
using System.Collections.Generic;
using System.Linq;
using SoldierInfoContext;


namespace ValorVault.Tests.Services
{
    [TestClass]
    public class DataVerificationServiceTests
    {
        [TestMethod]
        public void MarkSoldierInfoAsVerified_SoldierInfoExists_SavesChanges()
        {
            // Arrange
            var soldier_info_id = 1;
            var soldierInfo = new SoldierInfo { soldier_info_id = soldier_info_id };

            var dbContextMock = new Mock<SoldierInfoDbContext>();
            dbContextMock.Setup(x => x.soldier_infos.Find(soldier_info_id)).Returns(soldierInfo);

            var dataVerificationService = new DataVerificationService(dbContextMock.Object);

            // Act
            dataVerificationService.MarkSoldierInfoAsVerified(soldier_info_id);

            // Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void MarkSoldierInfoAsVerified_SoldierInfoDoesNotExist_DoesNotSaveChanges()
        {
            // Arrange
            var soldierInfoId = 1;

            var dbContextMock = new Mock<SoldierInfoDbContext>();
            dbContextMock.Setup(x => x.soldier_infos.Find(soldierInfoId)).Returns((SoldierInfo)null);

            var dataVerificationService = new DataVerificationService(dbContextMock.Object);

            // Act
            dataVerificationService.MarkSoldierInfoAsVerified(soldierInfoId);

            // Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void MarkSoldierInfoAsRejected_RemovesSoldierInfo_WhenSoldierInfoExists()
        {
            // Arrange
            var soldierInfoId = 1; // Припустимо, що ID військової інформації - 1

            var soldierInfo = new SoldierInfo { soldier_info_id = soldierInfoId /* Додайте інші необхідні властивості */ };

            var mockSet = new Mock<DbSet<SoldierInfo>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(soldierInfo); // Повертаємо військову інформацію з вказаним ID
            mockSet.Setup(m => m.Remove(It.IsAny<SoldierInfo>())).Verifiable(); // Налаштовуємо метод Remove для перевірки його виклику

            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(c => c.soldier_infos).Returns(mockSet.Object);

            var service = new DataVerificationService(mockContext.Object);

            // Act
            service.MarkSoldierInfoAsRejected(soldierInfoId);

            // Assert
            mockSet.Verify(m => m.Remove(soldierInfo), Times.Once); // Перевіряємо, чи викликався метод Remove з правильним об'єктом
            mockContext.Verify(m => m.SaveChanges(), Times.Once); // Перевіряємо, чи викликався метод SaveChanges
        }


        [TestMethod]
        public void MarkSoldierInfoAsRejected_SoldierInfoDoesNotExist_DoesNotRemoveOrSaveChanges()
        {
            // Arrange
            var soldierInfoId = 1;

            var soldierInfos = new List<SoldierInfo>(); // Порожній список солдатів

            var mockContext = new Mock<SoldierInfoDbContext>();
            var mockSet = new Mock<DbSet<SoldierInfo>>();

            // Підготовка макету DbSet
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.Provider).Returns(soldierInfos.AsQueryable().Provider);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.Expression).Returns(soldierInfos.AsQueryable().Expression);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.ElementType).Returns(soldierInfos.AsQueryable().ElementType);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.GetEnumerator()).Returns(soldierInfos.GetEnumerator());

            // Підготовка макету контексту бази даних
            mockContext.Setup(c => c.soldier_infos).Returns(mockSet.Object);

            var service = new DataVerificationService(mockContext.Object);

            // Act
            service.MarkSoldierInfoAsRejected(soldierInfoId);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<SoldierInfo>()), Times.Never);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}