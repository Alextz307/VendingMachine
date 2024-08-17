using Nagarro.VendingMachine.Domain.Report;
using System.Text.Json;
using System.Text;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    public class JsonReportSerializer : IReportSerializer
    {
        public string Serialize<T>(T report) where T : IReport
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                WriteIndented = true
            };

            byte[] utf8JsonBytes = JsonSerializer.SerializeToUtf8Bytes(report, jsonSerializerOptions);
            return Encoding.UTF8.GetString(utf8JsonBytes);
        }
    }
}
