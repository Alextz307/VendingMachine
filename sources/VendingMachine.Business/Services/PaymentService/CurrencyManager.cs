using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;

namespace Nagarro.VendingMachine.Business.Services.PaymentService
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly Dictionary<string, decimal> _currencies = new Dictionary<string, decimal>
        {
            {"500 lei", 500m},
            {"200 lei", 200m},
            {"100 lei", 100m},
            {"50 lei", 50m},
            {"20 lei", 20m},
            {"10 lei", 10m},
            {"5 lei", 5m},
            {"1 leu", 1m},
            {"50 bani", 0.5m},
            {"10 bani", 0.1m},
            {"5 bani", 0.05m},
            {"1 ban", 0.01m}
        };

        public bool CheckCurrency(string input)
        {
            return _currencies.ContainsKey(input);
        }

        public decimal GetValue(string currency)
        {
            return _currencies[currency];
        }

        public Dictionary<string, decimal> GetCurrencies()
        {
            return _currencies;
        }
    }
}