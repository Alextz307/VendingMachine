using Nagarro.VendingMachine.Business.UseCases;

namespace Nagarro.VendingMachine.Presentation.Commands
{
    public class LookCommand : IConsoleCommand
    {
        public string Name => "look";

        public string Description => "Look at the products.";

        public bool CanExecute => true;

        private readonly IUseCaseFactory _useCaseFactory;

        public LookCommand(IUseCaseFactory useCaseFactory)
        {
            _useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
        }

        public void Execute()
        {
            _useCaseFactory.Create<LookUseCase>().Execute();
        }
    }
}