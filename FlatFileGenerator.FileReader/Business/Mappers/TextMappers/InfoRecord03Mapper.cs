using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord03Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed03, InfoRecord03>(loggerFactory)
    {
    }
}
