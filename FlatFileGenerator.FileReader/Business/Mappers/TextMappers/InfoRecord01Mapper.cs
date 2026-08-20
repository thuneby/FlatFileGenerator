using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord01Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed01, InfoRecord01>(loggerFactory)
    {
    }
}
