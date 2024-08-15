using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.DataAccessInterfaces
{
    public interface ISaleRepository
    {
        void Add(Sale sale);

        IEnumerable<Sale> GetAll();

        IEnumerable<Sale> GetSalesByTimeInterval(DateTime? startTime, DateTime? endTime);

        IEnumerable<Product> GetSoldProductsByTimeInterval(DateTime? startTime, DateTime? endTime);
    }
}
