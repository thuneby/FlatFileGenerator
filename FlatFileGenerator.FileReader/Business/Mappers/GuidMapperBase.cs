using AutoMapper;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileReader.Business.Mappers
{
    public abstract class GuidMapperBase<T1, T2>
        where T1 : GuidModelBase
        where T2 : GuidModelBase
    {
        protected MapperConfiguration MapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>());
        protected IMapper Mapper;

        protected GuidMapperBase()
        {
            Mapper = MapperConfiguration.CreateMapper();
        }

        public T2 GetRecord(T1 record)
        {
            var result = Mapper.Map<T1, T2>(record);
            return result;
        }
    }
}
