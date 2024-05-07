using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValorVault.Controllers;
using ValorVault.Models;
using ValorVault.Services;
using Microsoft.Extensions.DependencyInjection; // Додайте цей using для методу AddDbContext
using SoldierInfoContext;

namespace ValorVault.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTests
    {
        [TestMethod]
        public async Task SearchByName_ReturnsOkResult_WithMatchingSoldiers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SoldierInfoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new SoldierInfoDbContext(options))
            {
                // Додаємо тестових солдатів до пам'яті
                context.SoldierInfos.AddRange(new List<SoldierInfo>
                {
                    new SoldierInfo { soldier_name = "Clark Doe", birth_place = "Some place", call_sign = "Some sign", death_place = "Some place", missing_place = "Some place", other_info = "Some info", photo = new byte[0], profile_status = "Some status", rank = "Some rank", soldier_status = "Some status" },
                    new SoldierInfo { soldier_name = "Jane Smith", birth_place = "Some place", call_sign = "Some sign", death_place = "Some place", missing_place = "Some place", other_info = "Some info", photo = new byte[0], profile_status = "Some status", rank = "Some rank", soldier_status = "Some status" },
                    new SoldierInfo { soldier_name = "David Johnson", birth_place = "Some place", call_sign = "Some sign", death_place = "Some place", missing_place = "Some place", other_info = "Some info", photo = new byte[0], profile_status = "Some status", rank = "Some rank", soldier_status = "Some status" }
                });
                context.SaveChanges();
            }

            var serviceProvider = new ServiceCollection()
                .AddDbContext<SoldierInfoDbContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"))
                .AddScoped<ISearchService, SearchService>()
                .BuildServiceProvider();

            var searchService = serviceProvider.GetService<ISearchService>();
            var controller = new SearchController(searchService);

            // Act
            var result = await controller.SearchByName("Clark");

            // Assert
            var okResult = result.Result as OkObjectResult;

            // Перевірка, чи можна привести результат до типу IEnumerable<SoldierInfo>
            var soldiers = okResult.Value as IEnumerable<SoldierInfo>;

            // Перевірка, чи в колекції є тільки один солдат з ім'ям "John Doe"
            Assert.AreEqual(1, soldiers.Count());
            Assert.AreEqual("Clark Doe", soldiers.First().soldier_name);
        }
    }
}