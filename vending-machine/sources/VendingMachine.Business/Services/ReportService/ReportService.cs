using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.Services.FileService;
using Nagarro.VendingMachine.Domain.Report;
using System.Configuration;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IReportSerializer _reportSerializer;
        private readonly IFileService _fileService;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IReportSerializer reportSerializer, IFileService fileService, ILogger<ReportService> logger) 
        {
            _reportSerializer = reportSerializer ?? throw new ArgumentNullException(nameof(reportSerializer));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private static string ConstructFileName(string reportType)
        {
            string? extension = ConfigurationManager.AppSettings["ReportType"];
            return $"{reportType} Report - {DateTime.Now:yyyy MM dd HHmmss}.{extension}";
        }

        public void Execute<T>(T report) where T : IReport
        {
            string content = _reportSerializer.Serialize(report);

            string fileName = ConstructFileName(report.Name);

            _fileService.Save(fileName, content);

            _logger.Info("User generated a " + ConfigurationManager.AppSettings["ReportType"] + " report for " + report.Name);
        }
    }
}
