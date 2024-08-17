namespace Nagarro.VendingMachine.Business.Services.FileService
{
    public interface IFileService
    {
        void Save(string fileName, string content);
    }
}