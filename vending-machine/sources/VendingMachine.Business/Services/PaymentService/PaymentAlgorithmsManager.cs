using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;

namespace Nagarro.VendingMachine.Business.Services.PaymentService
{
    public class PaymentAlgorithmsManager : IPaymentAlgorithmsManager
    {
        private readonly IEnumerable<IPaymentAlgorithm> _paymentAlgorithms;

        public PaymentAlgorithmsManager(IEnumerable<IPaymentAlgorithm> paymentAlgorithms)
        {
            _paymentAlgorithms = paymentAlgorithms;
        }

        public IEnumerable<IPaymentAlgorithm> GetPaymentAlgorithms()
        {
            return _paymentAlgorithms;
        }

        public IPaymentAlgorithm GetPaymentAlgorithmByName(string name)
        {
            return _paymentAlgorithms.First(paymentAlgorithm => paymentAlgorithm.Name == name);
        }
    }
}