using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.UseCases.Reports;

namespace Nagarro.VendingMachine.Presentation.Commands.Reports
{
    public class StockReportCommand : IConsoleCommand
    {
        private readonly IAuthenticationService _authenticationService;

        public string Name => "stock";

        public string Description => "Create a report of all products available in stock.";

        public bool CanExecute => _authenticationService.IsUserAuthenticated;

        private readonly IUseCaseFactory _useCaseFactory;

        public StockReportCommand(IAuthenticationService authenticationService, IUseCaseFactory useCaseFactory)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            _useCaseFactory.Create<StockReportUseCase>().Execute();
        }
    }
}
