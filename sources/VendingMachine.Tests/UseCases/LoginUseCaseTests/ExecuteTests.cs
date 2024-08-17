using Moq;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    public class ExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILoginView> loginView;
        private readonly Mock<ILogger<LoginUseCase>> logger;
        private readonly LoginUseCase loginUsecase;

        public ExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILoginView>();
            logger = new Mock<ILogger<LoginUseCase>>();

            loginUsecase = new LoginUseCase(authenticationService.Object, loginView.Object, logger.Object);
        }

        [Fact]
        public void HavingALoginUseCaseInstance_WhenExecuted_TheUserIsAskedToInputPassword()
        {
            loginUsecase.Execute();

            loginView.Verify(x => x.AskForPassword(), Times.Once);
        }

        [Fact]
        public void HavingALoginUseCaseInstance_WhenExecuted_TheUserIsAuthenticated()
        {
            loginView.Setup(x => x.AskForPassword()).Returns("parola");

            loginUsecase.Execute();

            authenticationService.Verify(x => x.Login("parola"), Times.Once);
        }
    }
}