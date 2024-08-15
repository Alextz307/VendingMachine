using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    public class Sales
    {
        [XmlElement("Product")]
        public List<Product>? Products { get; set; }
    }
}
