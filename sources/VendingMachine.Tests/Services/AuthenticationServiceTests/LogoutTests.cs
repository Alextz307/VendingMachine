using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Tests.Services.AuthenticationServiceTests
{
    public class LogoutTests
    {
        private const string CorrectPassword = "Alex";

        [Fact]
        public void HavingAnAuthenticatedUser_WhenLogout_ThenUserIsNotAuthenticated()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.Login(CorrectPassword);


            authenticationService.Logout();

            Assert.False(authenticationService.IsUserAuthenticated);

        }

        [Fact]
        public void HavingAnUnAuthenticatedUser_WhenLogout_ThenUserIsNotAuthenticated()
        {
            var authenticationService = new AuthenticationService();

            authenticationService.Logout();

            Assert.False(authenticationService.IsUserAuthenticated);
        }
    }
}