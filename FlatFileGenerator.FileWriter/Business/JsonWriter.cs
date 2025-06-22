using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlatFileGenerator.FileWriter.Business
{
    public class JsonWriter: IAsyncWriter
    {
        public async Task<bool> WriteAsync(IEnumerable<ReceiptDetail> records, string fileName, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(records.ToList(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

            });

            var util = new FileUtil();
            var success = await util.WriteFileAsync(jsonString, fileName, filePath);
            return success;
        }
    }
}
