using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.UseCases;

namespace Nagarro.VendingMachine.Presentation.Commands
{
    public class BuyCommand : IConsoleCommand
    {
        private readonly IAuthenticationService _authenticationService;

        public string Name => "buy";

        public string Description => "Choose a product to buy.";

        public bool CanExecute => !_authenticationService.IsUserAuthenticated;

        private readonly IUseCaseFactory _useCaseFactory;

        public BuyCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            _useCaseFactory.Create<BuyUseCase>().Execute();
        }
    }
}