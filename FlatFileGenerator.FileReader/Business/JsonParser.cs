using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileReader.Interfaces;
using System.Text.Json;

namespace FlatFileGenerator.FileReader.Business
{
    public class JsonParser: IAsyncParser
    {
        public async Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var receiptDetails = await JsonSerializer.DeserializeAsync<ReceiptDetail[]>(payload);
            if (receiptDetails != null && receiptDetails.Any())
            {
                return receiptDetails.ToList();
            }
            return new List<ReceiptDetail>();
        }
    }
}
