using Moq;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.UseCases.Reports;

namespace Nagarro.VendingMachine.Tests.UseCases.SalesReportUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<ISaleRepository> saleRepository;
        private readonly Mock<IReportService> reportService;
        private readonly Mock<IReportsView> reportsView;

        public ConstructorTests()
        {
            saleRepository = new Mock<ISaleRepository>();
            reportService = new Mock<IReportService>();
            reportsView = new Mock<IReportsView>();
        }

        [Fact]
        public void HavingNullSaleRepository_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SalesReportUseCase(null, reportService.Object, reportsView.Object);
            });
        }

        [Fact]
        public void HavingNullReportService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SalesReportUseCase(saleRepository.Object, null, reportsView.Object);
            });
        }

        [Fact]
        public void HavingNullReportsView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SalesReportUseCase(saleRepository.Object, reportService.Object, null);
            });
        }
    }
}
