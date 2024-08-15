using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    public class Product
    {
        [XmlElement("Name")]
        public string? Name { get; set; }

        [XmlElement("Quantity")]
        public int Quantity { get; set; }
    }
}
