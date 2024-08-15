using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Domain.Report;

namespace Nagarro.VendingMachine.Business.UseCases.Reports
{
    public class StockReportUseCase : IUseCase
    {
        private readonly IProductRepository _productRepository;

        private readonly IReportService _reportService;

        private readonly IReportsView _reportsView;

        public StockReportUseCase(IProductRepository productRepository, IReportService reportService, IReportsView reportsView) 
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _reportsView = reportsView ?? throw new ArgumentNullException(nameof(reportsView));
        }

        public void Execute()
        {
            List<Product> availableProducts = _productRepository.GetAllInStock()
                .Select(product => new Product { Name = product.Name, Quantity = product.Quantity })
                .ToList();

            _reportService.Execute(new StockReport { Products = availableProducts });

            _reportsView.DisplaySuccessMessage();
        }
    }
}
