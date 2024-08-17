using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.DataAccess.SQLite
{
    public class SqliteDbSaleRepositoryEF : ISaleRepository, IDisposable
    {
        private readonly VendingMachineContext _context;

        private bool _disposed;

        public SqliteDbSaleRepositoryEF(VendingMachineContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }   

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Add(Sale newSale)
        {
            _context.Sales.Add(newSale);
            _context.SaveChanges();
        }

        public IEnumerable<Sale> GetAll()
        {
            return _context.Sales.ToList();
        }

        public IEnumerable<Sale> GetSalesByTimeInterval(DateTime? startTime, DateTime? endTime)
        {
            return _context.Sales.Where(sale => sale.Date >= startTime && sale.Date <= endTime).ToList();
        }

        public IEnumerable<Product> GetSoldProductsByTimeInterval(DateTime? startTime, DateTime? endTime)
        {
            return _context.Sales
                .Where(sale => startTime <= sale.Date && sale.Date <= endTime)
                .GroupBy(sale => sale.Name)
                .Select(group => new Product { Name = group.Key, Quantity = group.Count() })
                .ToList();
        }
    }
}
