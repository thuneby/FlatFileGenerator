using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord04Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed04, InfoRecord04>(loggerFactory)
    {
    }
}
 