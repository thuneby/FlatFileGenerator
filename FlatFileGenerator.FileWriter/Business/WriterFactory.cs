using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Interfaces;

namespace FlatFileGenerator.FileWriter.Business
{
    public static class WriterFactory
    {
        public static IAsyncWriter GetWriter(DocumentType documentType, string batchNumber = "1000000000", string bankAccount = "12345678")
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonWriter();
                case DocumentType.NetsIs:
                    return new NetsIsWriter(batchNumber, bankAccount);
                case DocumentType.IpStandard:
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
