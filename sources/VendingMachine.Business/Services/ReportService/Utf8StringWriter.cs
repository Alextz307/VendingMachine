using System.Text;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
