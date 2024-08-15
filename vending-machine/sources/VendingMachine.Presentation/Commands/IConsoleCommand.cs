namespace Nagarro.VendingMachine.Presentation.Commands
{
    public interface IConsoleCommand
    {
        string Name { get; }

        string Description { get; }

        bool CanExecute { get; }

        void Execute();
    }
}