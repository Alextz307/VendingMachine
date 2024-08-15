using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;

namespace Nagarro.VendingMachine.Presentation.Views
{
    public class BuyView : DisplayBase, IBuyView
    {
        public int RequestId()
        {
            while (true)
            {
                Display("Enter the id of the product that you want to buy (positive integer): ", ConsoleColor.Cyan);

                if (int.TryParse(Console.ReadLine(), out int id) && id >= 0)
                {
                    return id;
                }

                Display("You should enter a positive integer as an id!\n", ConsoleColor.Red);
            }
        }

        public bool ConfirmPay(string productName)
        {
            while (true)
            {
                Display($"You are about to buy {productName}. Do you want to proceed further with this transaction? (type 'yes' or 'no'): ", ConsoleColor.Cyan);
                string? answer = Console.ReadLine()?.ToLower();

                if (answer == "yes" || answer == "no")
                {
                    return answer == "yes";
                }

                Display("You should answer the question with 'yes' or 'no'!\n", ConsoleColor.Red);
            }
        }

        public void DispenseProduct(string productName)
        {
            Display($"Here is your {productName}\n", ConsoleColor.Cyan);
        }

        public void DisplayPaymentMethods(IEnumerable<IPaymentAlgorithm> paymentAlgorithms)
        {
            Display("Choose a payment method from the list by typing its name:\n", ConsoleColor.Cyan);

            foreach (IPaymentAlgorithm paymentMethod in paymentAlgorithms)
            {
                Display($"{paymentMethod.Name}\n", ConsoleColor.Cyan);
            }
        }

        public string? AskForPaymentMethod(IEnumerable<IPaymentAlgorithm> paymentAlgorithms)
        {
            while (true)
            {
                Display("Enter the name of the preffered payment method or type 'cancel' if you want " +
                        "to cancel the transaction: ", ConsoleColor.Cyan);

                string? input = Console.ReadLine()?.ToLower();

                if (input == "cancel")
                {
                    return null;
                }

                if (paymentAlgorithms.Any(paymentAlgorithm => paymentAlgorithm.Name == input))
                {
                    return input;
                }

                Display("You entered an invalid command!\n", ConsoleColor.Red);
            }
        }

        public void DisplayTransactionCanceledMessage()
        {
            Display("The transaction was canceled!\n", ConsoleColor.Red);
        }
    }
}