namespace Nagarro.VendingMachine.Business.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }

        void Login(string password);

        void Logout();
    }
}