using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    public interface IReportService
    {
        void Execute<T>(T report) where T : IReport;
    }
}
