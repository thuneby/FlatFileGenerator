using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers;
using FlatFileGenerator.FileReader.Business.Mappers.TextMappers;
using FlatFileGenerator.FileReader.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business
{
    public static class ParserFactory
    {
        public static IAsyncParser GetParser(DocumentType documentType, ILoggerFactory loggerFactory)
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonParser();
                case DocumentType.IpStandard:
                    return new IpStandardParser(new IpStandardParserHelper(), new IpStandardMapper(loggerFactory), new IpRecordMapper(loggerFactory));
                case DocumentType.NetsIs:
                    return new NetsIsParser(loggerFactory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
