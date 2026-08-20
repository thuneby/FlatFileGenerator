using AutoMapper;
using FlatFileGenerator.Core.Models;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers
{
    public class MinimumReceiptDetailMapper: GuidMapperBase<ReceiptDetail, ReceiptDetail>
    {
        public MinimumReceiptDetailMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            MapperConfiguration = GetMapperConfiguration(loggerFactory);
            Mapper = MapperConfiguration.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ReceiptDetail, ReceiptDetail>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                    .ForMember(dest => dest.LaborAgreementNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.PolicyNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.TotalContributionRate, opt => opt.Ignore())
                    .ForMember(dest => dest.EmployerContributionRate, opt => opt.Ignore())
                    .ForMember(dest => dest.EmployerContribution, opt => opt.Ignore())
                    .ForMember(dest => dest.ContributionRateFromDate, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalContribution, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalContributionStartDate, opt => opt.Ignore())
                    .ForMember(dest => dest.EmploymentTerminationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.DeviationStartDate, opt => opt.Ignore())
                    .ForMember(dest => dest.DeviationEndDate, opt => opt.Ignore())
                    .ForMember(dest => dest.DeviationCode, opt => opt.Ignore())
                    .ForMember(dest => dest.EmployeeSalaryStartDate, opt => opt.Ignore())
                    .ForMember(dest => dest.EmployeeSalary, opt => opt.Ignore())
                    .ForMember(dest => dest.TermsOfSalary, opt => opt.Ignore())
                    .ForMember(dest => dest.EmploymentRateStartDate, opt => opt.Ignore())
                    .ForMember(dest => dest.EmploymentRate, opt => opt.Ignore())
                , loggerFactory);
            return config;

        }
    }
}
