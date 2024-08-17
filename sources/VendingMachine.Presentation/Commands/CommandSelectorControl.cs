namespace Nagarro.VendingMachine.Presentation.Commands
{
    internal class CommandSelectorControl : DisplayBase
    {
        public IEnumerable<IConsoleCommand>? Commands { get; set; }

        public IConsoleCommand Display()
        {
            DisplayCommands();
            return SelectCommand();
        }

        private void DisplayCommands()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine();

            if (Commands != null)
            {
                foreach (IConsoleCommand command in Commands)
                {
                    DisplayCommand(command);
                }
            }
        }

        private static void DisplayCommand(IConsoleCommand command)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(command.Name);

            Console.ForegroundColor = oldColor;

            Console.WriteLine(" - " + command.Description);
        }

        private IConsoleCommand SelectCommand()
        {
            while (true)
            {
                string? rawValue = ReadCommandName();
                IConsoleCommand? selectedCommand = FindCommand(rawValue);

                if (selectedCommand != null)
                {
                    return selectedCommand;
                }

                DisplayLine("Invalid command. Please try again.", ConsoleColor.Red);
            }
        }

        private IConsoleCommand? FindCommand(string? rawValue)
        {
            return Commands?.FirstOrDefault(command => command.Name == rawValue);
        }

        private string? ReadCommandName()
        {
            Console.WriteLine();
            Display("Choose command: ", ConsoleColor.Cyan);
            string? rawValue = Console.ReadLine();
            Console.WriteLine();

            return rawValue;
        }
    }
}