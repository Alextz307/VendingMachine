using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;

namespace Nagarro.VendingMachine.Presentation.Views
{
    public class ShelfView : DisplayBase, IShelfView
    {
        public void DisplayProducts(IEnumerable<Product> products)
        {
            string emptyLines = string.Empty;

            foreach (Product product in products)
            { 
                string productString = $"{emptyLines}Product id: {product.Id}\nProduct name: {product.Name}\nPrice: {product.Price}\nAvailable quantity: {product.Quantity}";

                Display(productString, ConsoleColor.Cyan);

                emptyLines = "\n\n";
            }
        }
    }
}