using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord00Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed00, InfoRecord00>(loggerFactory)
    {
    }
}
