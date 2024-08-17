using Moq;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.UseCases.Reports;

namespace Nagarro.VendingMachine.Tests.UseCases.StockReportUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IReportService> reportService;
        private readonly Mock<IReportsView> reportsView;

        public ConstructorTests()
        {
            productRepository = new Mock<IProductRepository>();
            reportService = new Mock<IReportService>();
            reportsView = new Mock<IReportsView>();
        }

        [Fact]
        public void HavingNullProductRepository_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StockReportUseCase(null, reportService.Object, reportsView.Object);
            });
        }

        [Fact]
        public void HavingNullReportService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StockReportUseCase(productRepository.Object, null, reportsView.Object);
            });
        }

        [Fact]
        public void HavingNullReportsView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StockReportUseCase(productRepository.Object, reportService.Object, null);
            });
        }
    }
}
