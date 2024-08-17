namespace Nagarro.VendingMachine.Business.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string productName)
            : base(FormatExceptionMessage(productName))
        {
        }

        private static string FormatExceptionMessage(string productName)
        {
            return $"The requested product ({productName}) is out of stock!";
        }
    }
}