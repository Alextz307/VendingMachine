using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;

namespace Nagarro.VendingMachine.Business.PresentationInterfaces.Views
{
    public interface IBuyView
    {
        bool ConfirmPay(string productName);

        void DispenseProduct(string productName);

        int RequestId();

        void DisplayPaymentMethods(IEnumerable<IPaymentAlgorithm> paymentMethods);

        string? AskForPaymentMethod(IEnumerable<IPaymentAlgorithm> paymentMethods);

        void DisplayTransactionCanceledMessage();
    }
}