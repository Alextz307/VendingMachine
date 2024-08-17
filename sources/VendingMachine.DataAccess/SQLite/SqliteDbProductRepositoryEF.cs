using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Nagarro.VendingMachine.DataAccess.SQLite
{
    public class SqliteDbProductRepositoryEF : IProductRepository, IDisposable
    {
        private readonly VendingMachineContext _context;

        private bool _disposed;

        public SqliteDbProductRepositoryEF(VendingMachineContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!_context.Products.Any())
            {
                StoreInitialData();
            }
        }

        private void StoreInitialData()
        {
            IEnumerable<Product> initialProducts = new List<Product>
            {
                new() { Id = 1, Name = "Chocolate", Price = 9, Quantity = 20 },
                new() { Id = 2, Name = "Chips", Price = 5, Quantity = 7 },
                new() { Id = 3, Name = "Still water", Price = 2, Quantity = 10 },
                new() { Id = 4, Name = "Coca-Cola", Price = 4, Quantity = 0 },
                new() { Id = 5, Name = "Pepsi", Price = 4, Quantity = 1 }
            };

            _context.Products.AddRange(initialProducts);
            _context.SaveChanges();
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

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetAllInStock()
        {
            return _context.Products.Where(p => p.Quantity > 0).ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void DecrementStock(Product product)
        {
            Product? productToUpdate = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate.Quantity -= 1;
                _context.Entry(productToUpdate).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            Product? productToUpdate = _context.Products.FirstOrDefault(p => p.Id == id);
            
            if (productToUpdate == null)
            {
                return false;
            }

            _context.Entry(productToUpdate).CurrentValues.SetValues(updatedProduct);
            _context.Entry(updatedProduct).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public void AddProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        public bool DeleteProduct(int id)
        {
            Product? productToDelete = GetById(id);

            if (productToDelete == null)
            {
                return false; 
            }

            _context.Products.Remove(productToDelete);
            _context.SaveChanges();

            return true;
        }
    }
}
