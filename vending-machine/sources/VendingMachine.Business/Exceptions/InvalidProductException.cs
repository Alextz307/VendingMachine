namespace Nagarro.VendingMachine.Business.Exceptions
{
    public class InvalidProductIdException : Exception
    {
        public InvalidProductIdException(int id)
            : base(FormatExceptionMessage(id))
        {
        }

        private static string FormatExceptionMessage(int id)
        {
            return $"There is no product with id {id}!";
        }
    }
}