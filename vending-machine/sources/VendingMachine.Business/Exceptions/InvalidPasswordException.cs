namespace Nagarro.VendingMachine.Business.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        private const string DefaultMessage = "Invalid password";

        public InvalidPasswordException()
            : base(DefaultMessage)
        {
        }
    }
}