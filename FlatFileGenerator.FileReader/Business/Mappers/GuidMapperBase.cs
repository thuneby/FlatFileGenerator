using AutoMapper;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileReader.Business.Mappers
{
    public abstract class GuidMapperBase<T1, T2>
        where T1 : GuidModelBase
        where T2 : GuidModelBase
    {
        private readonly IMapper _mapper;
        public MapperConfiguration MapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>());

        protected GuidMapperBase()
        {
            _mapper = MapperConfiguration.CreateMapper();
        }

        public T2 GetRecord(T1 record)
        {
            var result = _mapper.Map<T1, T2>(record);
            return result;
        }
    }
}
