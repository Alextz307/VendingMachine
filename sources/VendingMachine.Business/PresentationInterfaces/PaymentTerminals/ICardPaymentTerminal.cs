namespace Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals
{
    public interface ICardPaymentTerminal
    {
        string? AskForCardNumber();

        void DisplayTransactionCanceledMessage();

        void DisplayInvalidCardNumberMessage();

        void DisplaySuccessMessage();
    }
}