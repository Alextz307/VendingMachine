namespace Nagarro.VendingMachine.Business.Logging
{
    public interface ILogger<T>
    {
        void Info(string message);
    }
}