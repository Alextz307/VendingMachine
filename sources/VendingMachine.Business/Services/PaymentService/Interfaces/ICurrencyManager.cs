namespace Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces
{
    public interface ICurrencyManager
    {
        bool CheckCurrency(string input);

        Dictionary<string, decimal> GetCurrencies();

        decimal GetValue(string currency);
    }
}