using Moq;
using Nagarro.VendingMachine.Presentation.Commands;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Tests.Commands
{
    public class LoginCommandTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IUseCaseFactory> useCaseFactory;

        public LoginCommandTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            useCaseFactory = new Mock<IUseCaseFactory>();
        }

        [Fact]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginCommand(null, useCaseFactory.Object);
            });
        }

        [Fact]
        public void HavingNullUseCaseFactory_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginCommand(authenticationService.Object, null);
            });
        }

        [Fact]
        public void HavingNoAdminLoggedIn_CanExecuteIsTrue()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(false);

            LoginCommand loginCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.True(loginCommand.CanExecute);
        }

        [Fact]
        public void HavingAdminLoggedIn_CanExecuteIsFalse()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(true);

            LoginCommand loginCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.False(loginCommand.CanExecute);
        }

        [Fact]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            LoginCommand loginCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.Equal("login", loginCommand.Name);
        }

        [Fact]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            LoginCommand loginCommand = new(authenticationService.Object, useCaseFactory.Object);

            Assert.NotEmpty(loginCommand.Description);
        }
    }
}