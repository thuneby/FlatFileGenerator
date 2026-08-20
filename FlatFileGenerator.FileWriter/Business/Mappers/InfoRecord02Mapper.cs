using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoRecord02Mapper(ILoggerFactory loggerFactory): NetsIsMapperBase<InfoRecord02, InfoRecordFixed02>(loggerFactory)
    {
    }
}
