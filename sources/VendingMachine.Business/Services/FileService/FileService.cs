using System.Configuration;

namespace Nagarro.VendingMachine.Business.Services.FileService
{
    public class FileService : IFileService
    {
        public void Save(string fileName, string content)
        {
            File.WriteAllText(fileName, content);
        }
    }
}
