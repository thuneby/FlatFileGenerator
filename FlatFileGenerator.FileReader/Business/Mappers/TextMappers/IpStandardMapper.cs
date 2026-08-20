using FlatFileGenerator.Core.Models.IP.IPModels;
using FlatFileGenerator.Core.Models.IP.IPRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    public class IpStandardMapper(ILoggerFactory loggerFactory) : TextMapperBase<IpStandard, IpRecord>(loggerFactory)
    {
    }
}
