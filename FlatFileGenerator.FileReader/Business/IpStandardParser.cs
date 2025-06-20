using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.IP.IPModels;
using FlatFileGenerator.Core.Models.IP.IPRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers;
using FlatFileGenerator.FileReader.Interfaces;

namespace FlatFileGenerator.FileReader.Business
{
    public class IpStandardParser(FlatParserHelperBase<IpStandard> flatParserHelper, TextMapperBase<IpStandard, IpRecord> textMapper, GuidMapperBase<IpRecord, ReceiptDetail> recordMapper) 
        : SimpleParser<IpStandard, IpRecord>(flatParserHelper, textMapper, recordMapper), IAsyncParser
    {
    }
}
