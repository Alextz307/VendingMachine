using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.Business.DataAccessInterfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllInStock();

        Product? GetById(int id);

        void DecrementStock(Product product);

        bool UpdateProduct(int id, Product updatedProduct);

        void AddProduct(Product product);  

        bool DeleteProduct(int id);
    }
}