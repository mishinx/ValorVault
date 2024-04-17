using Moq;
using SoldierInfoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorVault.Services;

namespace Tests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        [TestMethod]
        public void Register_NewUser_ReturnsUser()
        {
            var mockSet = new List<User>().AsMockDbSet();
            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new RegistrationService(mockContext.Object);
            var newUser = new User { email = "new@example.com", password = "ValidPassword123", Name = "New User" };

            var result = service.Register(newUser.email, newUser.password, newUser.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(newUser.email, result.email);
            Assert.AreEqual(newUser.password, result.password);
            Assert.AreEqual(newUser.Name, result.Name);
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Register_ExistingEmail_ReturnsNull()
        {
            var existingUser = new User { email = "existing@example.com", password = "existingPassword", Name = "Existing User" };
            var mockSet = new List<User> { existingUser }.AsMockDbSet();
            var mockContext = new Mock<SoldierInfoDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new RegistrationService(mockContext.Object);
            var existingEmail = "existing@example.com";

            var result = service.Register(existingEmail, "password", "Name");

            Assert.IsNull(result);
        }
    }
}
