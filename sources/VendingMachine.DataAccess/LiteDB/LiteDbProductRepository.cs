using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;
using LiteDB;

namespace Nagarro.VendingMachine.DataAccess.LiteDB
{
    public class LiteDbProductRepository : IProductRepository
    {
        private readonly ILiteCollection<Product> _collection;

        public LiteDbProductRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            LiteDatabase database = new(connectionString);
            _collection = database.GetCollection<Product>();
        }

        public IEnumerable<Product> GetAll()
        {
            return _collection.FindAll();
        }

        public IEnumerable<Product> GetAllInStock()
        {
            return _collection.FindAll().Where(product => product.Quantity > 0);
        }

        public Product? GetById(int id)
        {
            return _collection.FindOne(p => p.Id == id);
        }

        public void DecrementStock(Product product)
        {
            Product? requestedProduct = GetById(product.Id);

            if (requestedProduct != null)
            {
                requestedProduct.Quantity -= 1;
                _collection.Update(requestedProduct);
            }
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        { 
            Product? existingProduct = GetById(id);

            if (existingProduct == null)
            {
                return false;
            }

            updatedProduct.Id = id;
            _collection.Update(updatedProduct);

            return true;
        }

        public void AddProduct(Product product)
        {
            _collection.Insert(product);
        }

        public bool DeleteProduct(int id)
        {
            return _collection.Delete(id);
        }
    }
}