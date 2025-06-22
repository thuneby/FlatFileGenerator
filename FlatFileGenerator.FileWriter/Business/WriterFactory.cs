using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileWriter.Interfaces;

namespace FlatFileGenerator.FileWriter.Business
{
    public class WriterFactory
    {
        public static IAsyncWriter GetWriter(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonWriter();
                case DocumentType.IpStandard:
                case DocumentType.NetsIs:
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
