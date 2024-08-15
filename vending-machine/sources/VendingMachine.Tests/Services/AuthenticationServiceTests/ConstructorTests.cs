using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Tests.Services.AuthenticationServiceTests
{
    public class ConstructorTests
    {
        [Fact]
        public void HavingAuthenticationServiceInstance_ThenUserIsNotAuthenticated()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            Assert.False(authenticationService.IsUserAuthenticated);
        }
    }
}