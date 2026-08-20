using AutoMapper;
using FlatFileGenerator.Core.Models;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.TextMappers
{
    public class TextMapperBase<T1, T2>
        where T1 : TextModelBase
        where T2 : GuidModelBase
    {
        protected IMapper Mapper;

        protected TextMapperBase(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>(), loggerFactory);
            Mapper = config.CreateMapper();
        }
        
        public T2 Map(T1 record)
        {
            var result = Mapper.Map<T1, T2>(record);
            return result;
        }

        public T2 GetRecord(object baseRecord)
        {
            var textRecord = (T1) baseRecord;
            var result = Map(textRecord);
            return result;
        }
    }
}
