namespace Nagarro.VendingMachine.Business.PresentationInterfaces.Views
{
    public interface IReportsView
    {
        bool AskForTimeInterval(out DateTime? startTime, out DateTime? endTime);

        void DisplayCanceledMessage();

        void DisplaySuccessMessage();
    }
}
