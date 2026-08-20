using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business
{
    public static class WriterFactory
    {
        public static IAsyncWriter GetWriter(DocumentType documentType, ILoggerFactory loggerFactory, string batchNumber = "1000000000", string bankAccount = "12345678")
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonWriter();
                case DocumentType.NetsIs:
                    return new NetsIsWriter(batchNumber, bankAccount, loggerFactory);
                case DocumentType.IpStandard:
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
