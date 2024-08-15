using System.Xml.Linq;

namespace Nagarro.VendingMachine.Business.DataAccessInterfaces
{
    public interface IReportRepository
    {
        void Add(XDocument xmlReport, string reportType);

        void Add(string jsonReport, string reportType);
    }
}
