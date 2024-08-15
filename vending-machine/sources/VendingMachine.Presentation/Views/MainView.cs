using Nagarro.VendingMachine.Presentation.Commands;

namespace Nagarro.VendingMachine.Presentation.Views
{
    public class MainView : DisplayBase, IMainView
    {
        public void DisplayApplicationHeader()
        {
            ApplicationHeaderControl applicationHeaderControl = new();
            applicationHeaderControl.Display();
        }

        public IConsoleCommand ChooseCommand(IEnumerable<IConsoleCommand> commands)
        {
            CommandSelectorControl commandSelectorControl = new()
            {
                Commands = commands
            };

            return commandSelectorControl.Display();
        }
    }
}