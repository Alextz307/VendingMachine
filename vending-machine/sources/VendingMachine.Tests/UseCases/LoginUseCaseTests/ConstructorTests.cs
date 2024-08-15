using Moq;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILoginView> loginView;
        private readonly Mock<ILogger<LoginUseCase>> logger;

        public ConstructorTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILoginView>();
            logger = new Mock<ILogger<LoginUseCase>>();
        }

        [Fact]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
           {
               new LoginUseCase(null, loginView.Object, logger.Object);
           });
        }

        [Fact]
        public void HavingNullLoginView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginUseCase(authenticationService.Object, null, logger.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginUseCase(authenticationService.Object, loginView.Object, null);
            });
        }
    }
}