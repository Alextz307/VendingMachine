using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.UseCases.Reports
{
    public class VolumeReportUseCase : IUseCase
    {
        private readonly ISaleRepository _saleRepository;

        private readonly IReportService _reportService;

        private readonly IReportsView _reportsView;

        public VolumeReportUseCase(ISaleRepository saleRepository, IReportService reportService, IReportsView reportsView)
        {
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _reportsView = reportsView ?? throw new ArgumentNullException(nameof(reportsView));
        }

        public void Execute()
        {
            if (!_reportsView.AskForTimeInterval(out DateTime? startTime, out DateTime? endTime))
            {
                _reportsView.DisplayCanceledMessage();
                return;
            }

            List<Product> soldProducts = _saleRepository.GetSoldProductsByTimeInterval(startTime, endTime).ToList();

            VolumeReport volumeReport = new()
            {
                StartTime = (DateTime)startTime,
                EndTime = (DateTime)endTime,
                Sales = new Sales { Products = soldProducts }
            };

            _reportService.Execute(volumeReport);

            _reportsView.DisplaySuccessMessage();
        }
    }
}
