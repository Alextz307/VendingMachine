using Moq;
using Nagarro.VendingMachine.Presentation.Commands.Reports;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Presentation.Commands;

namespace Nagarro.VendingMachine.Tests.Commands.Reports
{
    public class StockReportCommandTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IUseCaseFactory> useCaseFactory;
        private readonly StockReportCommand command;

        public StockReportCommandTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            useCaseFactory = new Mock<IUseCaseFactory>();
            command = new StockReportCommand(authenticationService.Object, useCaseFactory.Object);
        }

        [Fact]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StockReportCommand(null, useCaseFactory.Object);
            });
        }

        [Fact]
        public void HavingNullUseCaseFactory_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StockReportCommand(authenticationService.Object, null);
            });
        }

        [Fact]
        public void WhenInitializingTheCommand_NameIsCorrect()
        {
            Assert.Equal("stock", command.Name);
        }

        [Fact]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            Assert.NotEmpty(command.Description);
        }

        [Fact]
        public void HavingNoUserLoggedIn_CanExecuteIsTrue()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(false);

            Assert.False(command.CanExecute);
        }

        [Fact]
        public void HavingUserLoggedIn_CanExecuteIsFalse()
        {
            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(true);

            Assert.True(command.CanExecute);
        }
    }
}
