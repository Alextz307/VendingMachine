using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    public class Sale
    {
        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Name")]
        [Key]
        public string? Name { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }

        [XmlElement("PaymentMethod")]
        public string? PaymentMethod { get; set; }
    }
}
