namespace Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces
{
    public interface IPaymentAlgorithmsManager
    {
        IEnumerable<IPaymentAlgorithm> GetPaymentAlgorithms();

        IPaymentAlgorithm GetPaymentAlgorithmByName(string name);
    }
}