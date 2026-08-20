using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoSectionStartMapper(ILoggerFactory loggerFactory): NetsIsMapperBase<InfoSectionStart, InfoSectionStartRecord>(loggerFactory)
    {
    }
}
