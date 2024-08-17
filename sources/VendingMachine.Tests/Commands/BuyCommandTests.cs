using Moq;
using Nagarro.VendingMachine.Presentation.Commands;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Tests.Commands
{
    public class BuyCommandTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IUseCaseFactory> useCaseFactory;
        private readonly BuyCommand command;

        public BuyCommandTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            useCaseFactory = new Mock<IUseCaseFactory>();
            command = new(authenticationService.Object, useCaseFactory.Object);
        }

        [Fact]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyCommand(null, useCaseFactory.Object);
            });
        }

        [Fact]
        public void HavingNullUseCaseFactory_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyCommand(authenticationService.Object, null);
            });
        }

        [Fact]
        public void WhenInitializingTheCommand_NameIsCorrect()
        {
            Assert.Equal("buy", command.Name);
        }

        [Fact]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            Assert.NotEmpty(command.Description);
        }

        [Fact]
        public void HavingNoAdminLoggedIn_CanExecuteIsTrue()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(false);

            Assert.True(command.CanExecute);
        }

        [Fact]
        public void HavingAdminLoggedIn_CanExecuteIsFalse()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(true);

            Assert.False(command.CanExecute);
        }
    }
}