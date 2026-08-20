using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;


namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoRecord03Mapper(ILoggerFactory loggerFactory) : NetsIsMapperBase<InfoRecord03, InfoRecordFixed03>(loggerFactory)
    {
    }
}
