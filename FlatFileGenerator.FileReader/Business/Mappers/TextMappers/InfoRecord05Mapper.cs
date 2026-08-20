using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord05Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed05, InfoRecord05>(loggerFactory)
    {
    }
}
