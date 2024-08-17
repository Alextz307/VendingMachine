using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Nagarro.VendingMachine.Domain.Report
{
    [XmlRoot("VolumeReport")]
    public class VolumeReport : IReport
    {
        [XmlIgnore]
        [JsonIgnore]
        public string Name => "Volume";

        [XmlElement("StartTime")]
        public DateTime StartTime { get; set; }

        [XmlElement("EndTime")]
        public DateTime EndTime { get; set; }

        [XmlElement("Sales")]
        public Sales? Sales { get; set; }
    }
}
