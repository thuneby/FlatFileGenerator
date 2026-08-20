using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal class InfoEndMapper: NetsIsMapperBase<InfoEnd, InfoEndRecord>
    {
        public InfoEndMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
    }
}
