using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoSectionEndMapper(ILoggerFactory loggerFactory): NetsIsMapperBase<InfoSectionEnd, InfoSectionEndRecord>(loggerFactory)
    {
    }
}
