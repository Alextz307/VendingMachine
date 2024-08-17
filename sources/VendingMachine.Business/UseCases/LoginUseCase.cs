using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Business.UseCases
{
    public class LoginUseCase : IUseCase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoginView _loginView;
        private readonly ILogger<LoginUseCase> _logger;

        public LoginUseCase(IAuthenticationService authenticationService, ILoginView loginView, ILogger<LoginUseCase> logger)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _loginView = loginView ?? throw new ArgumentNullException(nameof(loginView));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            string password = _loginView.AskForPassword();
            _authenticationService.Login(password);

            _logger.Info("User logged in");
        }
    }
}