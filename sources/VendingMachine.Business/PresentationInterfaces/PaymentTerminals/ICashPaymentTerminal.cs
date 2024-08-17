namespace Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals   
{
    public interface ICashPaymentTerminal
    {
        void DisplayUnits();

        string? AskForMoney(decimal amount);

        void DisplayTransactionCanceledMessage();

        void DisplayInvalidCurrencyMessage();

        void GiveBackChange(int units, string currency);
    }
}