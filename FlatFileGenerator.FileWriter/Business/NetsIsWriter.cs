using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Business.Helpers;
using FlatFileGenerator.FileWriter.Interfaces;

namespace FlatFileGenerator.FileWriter.Business
{
    public class NetsIsWriter(string batchNumber, string bankAccount) : IAsyncWriter
    {
        public async Task<bool> WriteAsync(IEnumerable<ReceiptDetail> recordList, string fileName, string filePath)
        {
            var (netsModel,totalAmount) = CreateNetsIs.CreateNetsModel(recordList.ToList(), batchNumber, bankAccount);
            var payload = CreateNetsIs.CreatePayload(netsModel);
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
