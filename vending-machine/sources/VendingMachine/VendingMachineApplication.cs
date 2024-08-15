using System;
using System.Collections.Generic;
using Nagarro.VendingMachine.Presentation.Commands;

namespace Nagarro.VendingMachine
{
    internal class VendingMachineApplication : IApplication
    {
        private readonly IEnumerable<IConsoleCommand> _commands;
        private readonly IMainView _mainView;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(VendingMachineApplication));

        public VendingMachineApplication(IEnumerable<IConsoleCommand> commands, IMainView mainView)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
        }

        public void Run()
        {
            _mainView.DisplayApplicationHeader();

            while (true)
            {
                List<IConsoleCommand> availableCommands = GetExecutableCommands();

                IConsoleCommand command = _mainView.ChooseCommand(availableCommands);

                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    _mainView.DisplayError(ex.Message);
                    log.Error(ex.Message, ex);
                }
            }
        }

        private List<IConsoleCommand> GetExecutableCommands()
        {
            List<IConsoleCommand> executableCommands = new();

            foreach (IConsoleCommand command in _commands)
            {
                if (command.CanExecute)
                {
                    executableCommands.Add(command);
                }
            }

            return executableCommands;
        }
    }
}