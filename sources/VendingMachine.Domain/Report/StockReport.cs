using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    [XmlRoot("StockReport")]
    public class StockReport : IReport
    {
        [XmlIgnore]
        [JsonIgnore]
        public string Name => "Stock";

        [XmlElement("Product")]
        public List<Product>? Products { get; set; }
    }
}
