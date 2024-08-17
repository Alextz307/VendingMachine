using Moq;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    public class ExecuteTests
    {
        [Fact]
        public void HavingALogoutUseCaseInstance_WhenExecuted_ThenUserIsAuthenticated()
        {
            Mock<IAuthenticationService> authenticationService = new Mock<IAuthenticationService>();
            Mock<ILogger<LogoutUseCase>> logger = new Mock<ILogger<LogoutUseCase>>();
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object, logger.Object);

            logoutUseCase.Execute();

            authenticationService.Verify(x => x.Logout(), Times.Once);
        }
    }
}