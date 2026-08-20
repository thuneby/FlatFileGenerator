using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoRecord10Mapper(ILoggerFactory loggerFactory) : NetsIsMapperBase<InfoRecord10, InfoRecordFixed10>(loggerFactory)
    {
    }
}
