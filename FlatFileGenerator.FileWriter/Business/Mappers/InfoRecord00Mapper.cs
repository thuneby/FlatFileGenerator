using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoRecord00Mapper(ILoggerFactory loggerFactory): NetsIsMapperBase<InfoRecord00, InfoRecordFixed00>(loggerFactory)
    {
    }
}
