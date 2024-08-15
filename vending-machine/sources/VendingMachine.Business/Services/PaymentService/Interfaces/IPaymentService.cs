using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces
{
    public interface IPaymentService
    {
        bool Execute(Product product);
    }
}