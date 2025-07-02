using AutoMapper;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.FileReader.Business.Helpers;

namespace FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers
{
    public class InfoRecordMapper : GuidMapperBase<InfoRecord00, ReceiptDetail>
    {
        public InfoRecordMapper()
        {
            MapperConfiguration = GetMapperConfiguration();
            Mapper = MapperConfiguration.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InfoRecord00, ReceiptDetail>()
                .ForMember(dest => dest.Amount,
                    opt => opt.MapFrom(src => ConversionHelper.GetDecimal100(src.INDBET_BLB, src.INDBET_BLB_FRTFLT)))
                .ForMember(dest => dest.Cpr, opt => opt.MapFrom(src => ConversionHelper.CprHelper(src.KUND_CPR_NR)))
                .ForMember(dest => dest.Cvr, opt => opt.MapFrom(src => src.AFS_SE_NR))
                .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => "INFO-OVF"))
                .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ReceiptType, opt => opt.MapFrom(src => ReceiptType.Payment)) // ToDo
                .ForMember(dest => dest.LaborAgreementNumber, opt => opt.MapFrom(src => src.OVERENSKOMSTNR))
                .ForMember(dest => dest.FromDate,
                    opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PERIODE_FRA)))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PERIODE_TIL)))
                .ForMember(dest => dest.PersonFullName, opt => opt.MapFrom(src => GetName(src)))
                .ForMember(dest => dest.TotalContributionRate,
                    opt => opt.MapFrom(src => GetTotalContributionRate(src)))
                .ForMember(dest => dest.ContributionRateFromDate,
                    opt => opt.MapFrom(src => GetContributionRateFromDate(src)))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.KUND_NR_HOS_MODT))
                .ForMember(dest => dest.SubmissionDate,
                    opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(dest => dest.EmploymentTerminationDate, opt => opt.MapFrom(src => GetTerminationDate(src)))
            );
            return config;
        }

        private static string GetName(InfoRecord00 record)
        {
            var record01 = record.InfoRecord01.FirstOrDefault();
            if (record01 == null)
            {
                return string.Empty;
            }

            var name = record01.KUNDE_NAVN?.TrimEnd() ?? string.Empty;
            return name;
        }

        private static DateTime? GetTerminationDate(InfoRecord00 record)
        {
            var record01 = record.InfoRecord01.FirstOrDefault();
            if (record01 == null || string.IsNullOrWhiteSpace(record01.FRATR_DATO))
            {
                return null;
            }

            var terminationDate = ConversionHelper.ParseDate(record01.FRATR_DATO);
            if (ConversionHelper.IsMinDateTime(terminationDate))
            {
                return null;
            }

            return terminationDate;
        }

        private static Decimal? GetTotalContributionRate(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }

            return ConversionHelper.GetDecimal100(record02.PENS_BIDRAG_PCT);
        }

        private static DateTime? GetContributionRateFromDate(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }

            return ConversionHelper.ParseDate(record02.PENS_BIDRAG_PCT_START_DTO);
        }

    }
}

