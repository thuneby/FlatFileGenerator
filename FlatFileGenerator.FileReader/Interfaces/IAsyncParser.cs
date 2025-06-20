using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileReader.Interfaces
{
    public interface IAsyncParser
    {
        Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream reader, DocumentType documentType);
    }
}
