using Moq;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILogger<LogoutUseCase>> logger;

        public ConstructorTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            logger = new Mock<ILogger<LogoutUseCase>>();
        }

        [Fact]
        public void HavingANullAuthenticationService_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LogoutUseCase(null, logger.Object);
            });
        }

        [Fact]
        public void HavingANullLogger_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LogoutUseCase(authenticationService.Object, null);
            });
        }
    }
}