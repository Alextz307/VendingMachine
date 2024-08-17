using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;

namespace Nagarro.VendingMachine.Presentation.PaymentTerminals
{
    public class CashPaymentTerminal : DisplayBase, ICashPaymentTerminal
    {

        public void DisplayUnits()
        {
            Display("Please enter real unit of the money: 1 ban, 5 bani, 10 bani, 50 bani, 1 leu, 5 lei, 10 lei, 20 lei, " +
                    "50 lei, 100 lei, 200 lei, 500 lei.\n", ConsoleColor.Cyan);
        }

        public string? AskForMoney(decimal amount)
        {
            Display($"Please insert {amount} lei or type 'cancel' to abort the process and receive back your money: ", 
                    ConsoleColor.Cyan);

            string? input = Console.ReadLine()?.ToLower();

            if (input == "cancel")
            {
                return null;
            }
            
            return input; 
        }

        public void DisplayTransactionCanceledMessage()
        {
            Display("The transaction was canceled and your money were returned!\n", ConsoleColor.Red);
        }

        public void DisplayInvalidCurrencyMessage()
        {
            Display("You did not enter a valid banknote or coin!\n", ConsoleColor.Red);
        }

        public void GiveBackChange(int units, string currency)
        {
            Display($"Here are your {units} units of {currency}!\n", ConsoleColor.Green);
        }
    }
}