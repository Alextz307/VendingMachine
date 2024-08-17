using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.Business.PresentationInterfaces.Views
{
    public interface IShelfView
    {
        void DisplayProducts(IEnumerable<Product> products);
    }
}