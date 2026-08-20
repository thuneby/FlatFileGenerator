using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoSectionEndMapper(ILoggerFactory loggerFactory): TextMapperBase<InfoSectionEndRecord, InfoSectionEnd>(loggerFactory)
    {
    }
}
