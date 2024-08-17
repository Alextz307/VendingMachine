namespace Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces
{
    public interface IPaymentAlgorithm
    {
        string Name { get; }

        bool Run(decimal price);
    }
}