namespace Nagarro.VendingMachine.Presentation.Commands
{
    public interface IMainView
    {
        IConsoleCommand ChooseCommand(IEnumerable<IConsoleCommand> useCases);

        void DisplayApplicationHeader();

        void DisplayError(string errorMessage);
    }
}