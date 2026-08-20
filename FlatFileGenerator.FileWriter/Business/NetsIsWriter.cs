using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Business.Helpers;
using FlatFileGenerator.FileWriter.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business
{
    public class NetsIsWriter(string batchNumber, string bankAccount, ILoggerFactory loggerFactory) : IAsyncWriter
    {
        public async Task<bool> WriteAsync(IEnumerable<ReceiptDetail> recordList, string fileName, string filePath)
        {
            var creator = new CreateNetsIs(loggerFactory);
            var (netsModel,totalAmount) = creator.CreateNetsModel(recordList.ToList(), batchNumber, bankAccount);
            var payload = creator.CreatePayload(netsModel);
            await WritePayloadToFile(payload, fileName, filePath);
            return true;
        }

        private static async Task WritePayloadToFile(byte[] payload, string fileName, string filePath)
        {
            if (payload.Length == 0)
                return;
            //fileName = "IN" + batchNumber + ".txt"; // ToDo
            // Write the payload to the specified file path
            var util = new FileUtil();
            await util.WriteFileAsync(payload, fileName, filePath);
        }
    }
}
