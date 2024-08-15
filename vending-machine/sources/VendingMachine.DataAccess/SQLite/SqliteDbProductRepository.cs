using System.Data; 
using Microsoft.Data.Sqlite;
using Dapper;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.DataAccess.SQLite
{
    public class SqliteDbProductRepository : IProductRepository, IDisposable
    {
        private readonly IDbConnection _dbConnection;

        private bool _disposed;

        public SqliteDbProductRepository(string connectionString)
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

        public IEnumerable<Product> GetAll()
        {
            return _dbConnection.Query<Product>("SELECT * FROM Products");
        }

        public IEnumerable<Product> GetAllInStock()
        {
            return _dbConnection.Query<Product>("SELECT * FROM Products WHERE Quantity > 0");
        }

        public Product? GetById(int id)
        {
            return _dbConnection.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }

        public void DecrementStock(Product product)
        {
            _dbConnection.Execute("UPDATE Products SET Quantity = Quantity - 1 WHERE Id = @Id", new { product.Id });
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            updatedProduct.Id = id;

            int rowsAffected = _dbConnection.Execute("UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id", updatedProduct);

            return rowsAffected > 0;
        }

        public void AddProduct(Product product)
        {
            _dbConnection.Execute("INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)", product);
        }

        public bool DeleteProduct(int id)
        {
            int rowsAffected = _dbConnection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
            return rowsAffected > 0;
        }
    }
}