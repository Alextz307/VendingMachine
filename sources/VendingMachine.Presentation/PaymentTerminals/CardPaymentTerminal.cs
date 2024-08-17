using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;

namespace Nagarro.VendingMachine.Presentation.PaymentTerminals
{
    public class CardPaymentTerminal : DisplayBase, ICardPaymentTerminal
    {
        public string? AskForCardNumber()
        {
            while (true)
            {
                Display("If you want to cancel the transaction, type 'cancel'. Otherwise, please enter " +
                        "your card's number: ", ConsoleColor.Cyan);

                string? input = Console.ReadLine()?.ToLower();

                if (input == "cancel")
                {
                    return null;
                }

                if (string.IsNullOrEmpty(input) || !input.All(char.IsDigit))
                {
                    Display("A valid card number should consist only of digits!\n", ConsoleColor.Red);
                }
                else
                {
                    return input;
                }
            }
        }

        public void DisplayTransactionCanceledMessage()
        {
            Display("The transaction was canceled!", ConsoleColor.Red);
        }

        public void DisplayInvalidCardNumberMessage()
        {
            Display("You did not enter a valid card number!\n", ConsoleColor.Red);
        }

        public void DisplaySuccessMessage()
        {
            Display("Your transaction was approved!\n", ConsoleColor.Green);
        }
    }
}