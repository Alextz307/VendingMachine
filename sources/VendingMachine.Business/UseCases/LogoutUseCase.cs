using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;

namespace Nagarro.VendingMachine.Business.UseCases
{
    public class LogoutUseCase : IUseCase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<LogoutUseCase> _logger;

        public LogoutUseCase(IAuthenticationService authenticationService, ILogger<LogoutUseCase> logger)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            _authenticationService.Logout();

            _logger.Info("User logged out");
        }
    }
}