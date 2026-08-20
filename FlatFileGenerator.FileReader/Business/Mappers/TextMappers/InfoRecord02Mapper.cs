using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord02Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed02, InfoRecord02>(loggerFactory)
    {
    }
}
