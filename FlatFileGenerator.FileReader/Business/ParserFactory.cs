using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers;
using FlatFileGenerator.FileReader.Interfaces;

namespace FlatFileGenerator.FileReader.Business
{
    public static class ParserFactory
    {
        public static IAsyncParser GetParser(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonParser();
                case DocumentType.IpStandard:
                    return new IpStandardParser(new IpStandardParserHelper(), new IpStandardMapper(), new IpRecordMapper());
                case DocumentType.NetsIs:
                    return new NetsIsParser();
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
