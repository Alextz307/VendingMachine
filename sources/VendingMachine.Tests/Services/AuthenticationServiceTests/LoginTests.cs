using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.Exceptions;

namespace Nagarro.VendingMachine.Tests.Services.AuthenticationServiceTests
{
    public class LoginTests
    {
        private const string CorrectPassword = "Alex";

        [Fact]
        public void HavingAnAuthenticationService_WhenLoginWithCorrectPassword_ThenUserIsAuthenticated()
        {
            var authenticationService = new AuthenticationService();

            authenticationService.Login(CorrectPassword);

            Assert.True(authenticationService.IsUserAuthenticated);
        }

        [Fact]
        public void HavingAnAuthenticationService_WhenLoginWithInCorrectPassword_ThenThrowsException()
        {
            var authenticationService = new AuthenticationService();

            Assert.Throws<InvalidPasswordException>(() =>
            {
                authenticationService.Login("incorrect-password");
            });
        }

        [Fact]
        public void HavingAnAuthenticationService_WhenLoginWithIncorrectPassword_ThenUserIsNotAuthenticated()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            try
            {
                authenticationService.Login("incorrect-password");
            }
            catch
            {
            }

            Assert.False(authenticationService.IsUserAuthenticated);
        }

        [Fact]
        public void HavingAnAuthenticationService_WhenTryToLoginTwiceWithCorrectPassword_ThenUserIsAuthenticated()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            authenticationService.Login(CorrectPassword);
            authenticationService.Login(CorrectPassword);

            Assert.True(authenticationService.IsUserAuthenticated);
        }
    }
}