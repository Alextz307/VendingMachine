using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.UseCases.Reports
{
    public class SalesReportUseCase : IUseCase
    {
        private readonly ISaleRepository _saleRepository;

        private readonly IReportService _reportService;

        private readonly IReportsView _reportsView;

        public SalesReportUseCase(ISaleRepository saleRepository, IReportService reportService, IReportsView reportsView)
        {
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _reportsView = reportsView ?? throw new ArgumentNullException(nameof(reportsView));
        }

        public void Execute()
        {
            List<Sale> sales = _saleRepository.GetAll().ToList();

            _reportService.Execute(new SalesReport { Sales = sales });

            _reportsView.DisplaySuccessMessage();
        }
    }
}
