using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoStartMapper(ILoggerFactory loggerFactory): TextMapperBase<InfoStartRecord, InfoStart>(loggerFactory)
    {
    }
}
