using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using ValorVault.Services;
using ValorVault.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SoldierInfoContext;
using ValorVault.Models;

namespace Tests
{
    [TestClass]
    public class SoldierInfoServiceTests
    {
        private Mock<DbSet<SoldierInfo>> MockSet<T>(List<SoldierInfo> dataSource) where T : class
        {
            var data = dataSource.AsQueryable();
            var mockSet = new Mock<DbSet<SoldierInfo>>();
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SoldierInfo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private Mock<SoldierInfoDbContext> CreateMockContext(DbSet<SoldierInfo> set)
        {
            var mockContext = new Mock<SoldierInfoDbContext>(new DbContextOptions<SoldierInfoDbContext>());
            mockContext.Setup(c => c.soldier_infos).Returns(set);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);
            return mockContext;
        }

        [TestMethod]
        public void AddSoldierInfo_AddsInfoToDatabase()
        {
            var data = new List<SoldierInfo>();
            var mockSet = MockSet<SoldierInfo>(data);
            var mockContext = CreateMockContext(mockSet.Object);

            var service = new SoldierInfoService(mockContext.Object);
            var soldier = new SoldierInfo { soldier_info_id = 1, soldier_name = "Patrik Ohio" };

            service.AddSoldierInfo(soldier);

            mockSet.Verify(m => m.Add(It.IsAny<SoldierInfo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateSoldierInfo_UpdatesExistingInfo()
        {
            var soldier = new SoldierInfo { soldier_info_id = 1, soldier_name = "Patrik Ohio" };
            var data = new List<SoldierInfo> { soldier };
            var mockSet = MockSet<SoldierInfo>(data);
            var mockContext = CreateMockContext(mockSet.Object);

            var service = new SoldierInfoService(mockContext.Object);
            soldier.soldier_name = "Updated Name";

            service.UpdateSoldierInfo(soldier);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteSoldierInfo_RemovesInfoFromDatabase()
        {
            var soldier = new SoldierInfo { soldier_info_id = 1, soldier_name = "Patrik Ohio" };
            var data = new List<SoldierInfo> { soldier };
            var mockSet = MockSet<SoldierInfo>(data);

            // Налаштувати поведінку Find
            mockSet.Setup(m => m.Find(1)).Returns(soldier);

            var mockContext = CreateMockContext(mockSet.Object);
            var service = new SoldierInfoService(mockContext.Object);

            service.DeleteSoldierInfo(1);

            mockSet.Verify(m => m.Remove(It.IsAny<SoldierInfo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

    }
}
