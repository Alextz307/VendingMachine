using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;

namespace Nagarro.VendingMachine.Presentation.Views
{
    public class LoginView : DisplayBase, ILoginView
    {
        public string? AskForPassword()
        {
            Console.WriteLine();
            Display("Type the admin password: ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }
    }
}