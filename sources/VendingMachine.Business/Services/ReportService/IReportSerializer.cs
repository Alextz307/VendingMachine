using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    public interface IReportSerializer
    {
        string Serialize<T>(T report) where T : IReport;
    }
}
