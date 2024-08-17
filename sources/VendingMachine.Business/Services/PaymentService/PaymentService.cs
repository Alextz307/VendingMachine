using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;

namespace Nagarro.VendingMachine.Business.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IBuyView _buyView;
        private readonly IPaymentAlgorithmsManager _paymentAlgorithms;
        private readonly ISaleRepository _saleRepository;

        public PaymentService(IBuyView buyView, IPaymentAlgorithmsManager paymentAlgorithms, ISaleRepository saleRepository)
        {
            _buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            _paymentAlgorithms = paymentAlgorithms ?? throw new ArgumentNullException(nameof(paymentAlgorithms));
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        public bool Execute(Product product)
        {
            _buyView.DisplayPaymentMethods(_paymentAlgorithms.GetPaymentAlgorithms());
            
            string? input = _buyView.AskForPaymentMethod(_paymentAlgorithms.GetPaymentAlgorithms());

            if (input == null)
            {
                _buyView.DisplayTransactionCanceledMessage();
                return false;
            }

            _saleRepository.Add(new() { Date = DateTime.Now, Name = product.Name, Price = product.Price, PaymentMethod = input });
            
            return _paymentAlgorithms.GetPaymentAlgorithmByName(input).Run(product.Price);
        }
    }
}