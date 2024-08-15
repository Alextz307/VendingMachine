using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.UseCases;

namespace Nagarro.VendingMachine.Business.Services.PaymentService
{
    public class CardPayment : IPaymentAlgorithm
    {
        private readonly ICardPaymentTerminal _cardPaymentTerminal;
        private readonly ILogger<CardPayment> _logger;

        public string Name => "card";
        
        public CardPayment(ICardPaymentTerminal cardPaymentTerminal, ILogger<CardPayment> logger)
        {
            _cardPaymentTerminal = cardPaymentTerminal ?? throw new ArgumentNullException(nameof(cardPaymentTerminal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private static bool IsValidCardNumber(string input)
        {
            int checkSum = 0;

            for (int index = input.Length - 1; index >= 0; --index)
            {
                int digit = input[index] - '0';

                if ((index & 1) == (input.Length & 1))
                {
                    checkSum += digit > 4 ? 2 * digit - 9 : 2 * digit;
                }
                else
                {
                    checkSum += digit;
                }
            }

            return checkSum % 10 == 0;
        }

        public bool Run(decimal price)
        {
            while (true)
            {
                string? input = _cardPaymentTerminal.AskForCardNumber();

                if (input == null)
                {
                    _logger.Info("User canceled card payment");
                    _cardPaymentTerminal.DisplayTransactionCanceledMessage();
                    return false;
                }

                if (IsValidCardNumber(input))
                {
                    _logger.Info("User made a card payment");
                    _cardPaymentTerminal.DisplaySuccessMessage();
                    return true;
                }

                _cardPaymentTerminal.DisplayInvalidCardNumberMessage();
            }
        }
    }
}