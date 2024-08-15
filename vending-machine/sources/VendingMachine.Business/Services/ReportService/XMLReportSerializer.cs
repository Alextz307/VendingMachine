using Nagarro.VendingMachine.Domain.Report;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Business.Services.ReportService
{
    public class XMLReportSerializer : IReportSerializer
    {
        public string Serialize<T>(T report) where T : IReport
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            
            using (StringWriter utf8StringWriter = new Utf8StringWriter())
            {
                XmlWriterSettings xmlWriterSettings = new()
                {
                    Indent = true,
                    IndentChars = "\t",
                    Encoding = Encoding.UTF8
                };

                using (XmlWriter xmlWriter = XmlWriter.Create(utf8StringWriter, xmlWriterSettings))
                {
                    xmlSerializer.Serialize(xmlWriter, report);
                    return utf8StringWriter.ToString();
                }
            }
        }
    }
}
