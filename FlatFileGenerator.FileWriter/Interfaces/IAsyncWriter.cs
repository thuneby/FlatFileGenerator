using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileWriter.Interfaces
{
    public interface IAsyncWriter
    {
        Task<bool> WriteAsync(IEnumerable<ReceiptDetail> records, string fileName, string filePath);
    }
}
