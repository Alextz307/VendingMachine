using System;

[assembly: log4net.Config.XmlConfigurator(Watch=true)]

namespace Nagarro.VendingMachine
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        private static void Main()
        {
            log.Info("Application started");

            try
            {
                Bootstrapper.Run();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
                Pause();
            }
        }

        private static void DisplayError(Exception ex)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = oldColor;
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}