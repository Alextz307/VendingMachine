using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.UseCases;

namespace Nagarro.VendingMachine.Presentation.Commands
{
    public class LogoutCommand : IConsoleCommand
    {
        private readonly IAuthenticationService _authenticationService;

        public string Name => "logout";

        public string Description => "Restrict access to administration section.";

        public bool CanExecute => _authenticationService.IsUserAuthenticated;

        private readonly IUseCaseFactory _useCaseFactory;

        public LogoutCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            _useCaseFactory.Create<LogoutUseCase>().Execute();
        }
    }
}