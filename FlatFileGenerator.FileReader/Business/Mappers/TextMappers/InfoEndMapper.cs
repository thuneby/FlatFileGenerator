using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    internal class InfoEndMapper: TextMapperBase<InfoEndRecord, InfoEnd>
    {
        public InfoEndMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
    }
}
