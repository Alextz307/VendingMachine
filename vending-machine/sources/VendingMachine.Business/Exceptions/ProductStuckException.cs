namespace Nagarro.VendingMachine.Business.Exceptions
{
    public class ProductStuckException : Exception
    {
        public ProductStuckException(string productName)
            : base(FormatExceptionMessage(productName))
        {
        }

        private static string FormatExceptionMessage(string productName)
        {
            return $"Oh no, your product ({productName}) is stuck in the machine! Please contact the administrator!";
        }
    }
}