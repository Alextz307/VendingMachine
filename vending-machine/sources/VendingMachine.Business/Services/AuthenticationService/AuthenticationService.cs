using Nagarro.VendingMachine.Business.Exceptions;

namespace Nagarro.VendingMachine.Business.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsUserAuthenticated { get; private set; }

        public void Login(string password)
        {
            if (password == "Alex")
            {
                IsUserAuthenticated = true;
            }
            else
            {
                throw new InvalidPasswordException();
            }
        }

        public void Logout()
        {
            IsUserAuthenticated = false;
        }
    }
}