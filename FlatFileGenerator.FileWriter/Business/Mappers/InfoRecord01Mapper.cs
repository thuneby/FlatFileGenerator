using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoRecord01Mapper(ILoggerFactory loggerFactory): NetsIsMapperBase<InfoRecord01, InfoRecordFixed01>(loggerFactory)
    {
    }
}
