using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.DataAccess.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new()
        {
            new() { Id = 1, Name = "Chococolate", Price = 9, Quantity = 20 },
            new() { Id = 2, Name = "Chips", Price = 5, Quantity = 7 },
            new() { Id = 3, Name = "Still water", Price = 2, Quantity = 10 },
            new() { Id = 4, Name = "Coca-Cola", Price = 4, Quantity = 0 },
            new() { Id = 5, Name = "Pepsi", Price = 4, Quantity = 1 }
        };

        public IEnumerable<Product> GetAll()
        {
            return Products;
        }

        public IEnumerable<Product> GetAllInStock()
        {
            return Products.Where(product => product.Quantity > 0);
        }

        public Product? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public void DecrementStock(Product product)
        {
            product.Quantity -= 1;
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            Product? productToUpdate = GetById(id);

            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate.Name = updatedProduct.Name;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.Quantity = updatedProduct.Quantity;

            return true;
        }

        public void AddProduct(Product product)
        {
            int newId = Products.Max(p => p.Id) + 1;
            product.Id = newId;

            Products.Add(product);
        }

        public bool DeleteProduct(int id)
        {
            Product? productToDelete = Products.FirstOrDefault(p => p.Id == id);

            if (productToDelete == null)
            {
                return false;
            }

            Products.Remove(productToDelete);
            return true;
        }
    }
}