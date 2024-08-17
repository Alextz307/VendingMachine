using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain.Report;
using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;

namespace Nagarro.VendingMachine.DataAccess.SQLite
{
    public class SqliteDbSaleRepository : IDisposable, ISaleRepository
    {
        private readonly IDbConnection _dbConnection;

        private bool _disposed;

        public SqliteDbSaleRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _dbConnection = new SqliteConnection($"Data Source={connectionString}");
            _dbConnection.Open();
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
                _dbConnection?.Close();
                _dbConnection?.Dispose();
            }

            _disposed = true;
        }

        public void Add(Sale newSale)
        {
            string query = "INSERT INTO Sales (Date, Name, Price, PaymentMethod) VALUES (@Date, @Name, @Price, @PaymentMethod)";
            _dbConnection.Execute(query, newSale);
        }

        public IEnumerable<Sale> GetAll()
        {
            string query = "SELECT * FROM Sales";
            return _dbConnection.Query<Sale>(query);
        }

        public IEnumerable<Sale> GetSalesByTimeInterval(DateTime? startTime, DateTime? endTime)
        {
            string query = "SELECT * FROM Sales WHERE Date BETWEEN @StartTime AND @EndTime";
            return _dbConnection.Query<Sale>(query, new { StartTime = startTime, EndTime = endTime });
        }

        public IEnumerable<Product> GetSoldProductsByTimeInterval(DateTime? startTime, DateTime? endTime)
        {
            return GetSalesByTimeInterval(startTime, endTime)
                .GroupBy(sale => sale.Name)
                .Select(group => new Product { Name = group.Key, Quantity = group.Count() });
        }
    }
}
