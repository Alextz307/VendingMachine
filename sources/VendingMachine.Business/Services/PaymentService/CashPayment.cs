using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Business.Services.PaymentService
{
    public class CashPayment : IPaymentAlgorithm
    {
        private readonly ICashPaymentTerminal _cashPaymentTerminal;
        private readonly ICurrencyManager _currencyManager;
        private readonly ILogger<CashPayment> _logger;

        public string Name => "cash";

        public CashPayment(ICashPaymentTerminal cashPaymentTerminal, ICurrencyManager currencyManager, ILogger<CashPayment> logger)
        {
            _cashPaymentTerminal = cashPaymentTerminal ?? throw new ArgumentNullException(nameof(cashPaymentTerminal));
            _currencyManager = currencyManager ?? throw new ArgumentNullException(nameof(currencyManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private decimal ProcessInsertedMoney(decimal price)
        {   
            if (price > 0)
            {
                _cashPaymentTerminal.DisplayUnits();
            }

            decimal paidAmount = 0;

            while (paidAmount < price)
            {
                string? input = _cashPaymentTerminal.AskForMoney(price - paidAmount);

                if (input == null)
                {
                    _logger.Info("User canceled cash payment");
                    _cashPaymentTerminal.DisplayTransactionCanceledMessage();
                    return -1;
                }

                if (_currencyManager.CheckCurrency(input))
                {
                    paidAmount += _currencyManager.GetValue(input);
                }
                else
                {
                    _cashPaymentTerminal.DisplayInvalidCurrencyMessage();
                }
            }

            return paidAmount;
        }

        private void GiveBackChange(decimal amount)
        {
            Dictionary<string, decimal> currencies = _currencyManager.GetCurrencies();

            foreach (KeyValuePair<string, decimal> currency in currencies)
            {
                if (amount == 0)
                {
                    break;
                }

                int units = (int)(amount / currency.Value);

                if (units > 0)
                {
                    _cashPaymentTerminal.GiveBackChange(units, currency.Key);
                }

                amount -= units * currency.Value;
            }
        }

        public bool Run(decimal price)
        {
            decimal paidAmount = ProcessInsertedMoney(price);
           
            if (paidAmount == -1)
            {
                return false;
            }

            if (price < paidAmount)
            {
                GiveBackChange(paidAmount - price);
            }

            _logger.Info("User made a cash payment");

            return true;
        }
    }
}