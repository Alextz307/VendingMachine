using Moq;
using Nagarro.VendingMachine.Business.Services.FileService;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.Services.ReportServiceTests
{
    public class ConstructorTests
    {
        private readonly Mock<IReportSerializer> reportSerializer;
        private readonly Mock<IFileService> fileService;
        private readonly Mock<ILogger<ReportService>> logger;

        public ConstructorTests()
        {
            reportSerializer = new Mock<IReportSerializer>();
            fileService = new Mock<IFileService>();

            logger = new Mock<ILogger<ReportService>>();
        }

        [Fact]
        public void HavingNullReportSerializer_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ReportService(null, fileService.Object, logger.Object);
            });
        }

        [Fact]
        public void HavingNullFileService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ReportService(reportSerializer.Object, null, logger.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ReportService(reportSerializer.Object, fileService.Object, null);
            });
        }
    }
}
