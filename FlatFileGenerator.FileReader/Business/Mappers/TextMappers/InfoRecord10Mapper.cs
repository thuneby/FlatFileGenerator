using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoRecord10Mapper(ILoggerFactory loggerFactory): TextMapperBase<InfoRecordFixed10, InfoRecord10>(loggerFactory)
    {
    }
}
