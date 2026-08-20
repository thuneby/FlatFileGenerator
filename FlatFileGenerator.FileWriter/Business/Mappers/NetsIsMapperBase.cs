using AutoMapper;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Mappers
{
    internal abstract class NetsIsMapperBase<T1, T2>
        where T1 : GuidModelBase
        where T2 : NetsBase
    {
        private readonly IMapper _mapper;

        protected NetsIsMapperBase(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>(), loggerFactory);
            _mapper = config.CreateMapper();
        }
        
        public T2 GetRecord(T1 record)
        {
            var result = _mapper.Map<T1, T2>(record);
            return result;
        }
    }
}
