using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    [XmlRoot("SalesReport")]
    public class SalesReport : IReport
    {
        [XmlIgnore]
        [JsonIgnore]
        public string Name => "Sales";

        [XmlElement("Sale")]
        public List<Sale>? Sales { get; set; }
    }
}
