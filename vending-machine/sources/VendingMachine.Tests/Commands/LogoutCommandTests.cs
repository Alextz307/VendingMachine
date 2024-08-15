using Moq;
using Nagarro.VendingMachine.Presentation.Commands;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Tests.Commands
{
    public class LogoutCommandTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IUseCaseFactory> useCaseFactory;

        public LogoutCommandTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            useCaseFactory = new Mock<IUseCaseFactory>();
        }

        [Fact]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LogoutCommand(null, useCaseFactory.Object);
            });
        }

        [Fact]
        public void HavingNullUseCaseFactory_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LogoutCommand(authenticationService.Object, null);
            });
        }

        [Fact]
        public void HavingNoAdminLoggedIn_CanExecuteIsFalse()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(false);

            LogoutCommand logoutCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.False(logoutCommand.CanExecute);
        }

        [Fact]
        public void HavingAdminLoggedIn_CanExecuteIsTrue()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(true);

            LogoutCommand logoutCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.True(logoutCommand.CanExecute);
        }

        [Fact]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            LogoutCommand logoutCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.Equal("logout", logoutCommand.Name);
        }

        [Fact]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            LogoutCommand logoutCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.NotEmpty(logoutCommand.Description);
        }
    }
}