using AutoMapper;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.FileReader.Business.Helpers;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers
{
    public class InfoRecordMapper : GuidMapperBase<InfoRecord00, ReceiptDetail>
    {
        public InfoRecordMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            MapperConfiguration = GetMapperConfiguration(loggerFactory);
            Mapper = MapperConfiguration.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InfoRecord00, ReceiptDetail>()
                //.ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => DocumentType.NetsIs))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => ConversionHelper.GetDecimal100(src.INDBET_BLB, src.INDBET_BLB_FRTFLT)))
                .ForMember(dest => dest.Cpr, opt => opt.MapFrom(src => ConversionHelper.CprHelper(src.KUND_CPR_NR)))
                .ForMember(dest => dest.Cvr, opt => opt.MapFrom(src => src.AFS_SE_NR))
                .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => "INFO-OVF"))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.InfoSectionStart.INDBET_DTO)))
                .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ReceiptType, opt => opt.MapFrom(src => GetReceiptType(src)))
                .ForMember(dest => dest.LaborAgreementNumber, opt => opt.MapFrom(src => src.OVERENSKOMSTNR))
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PERIODE_FRA)))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PERIODE_TIL)))
                .ForMember(dest => dest.PersonFullName, opt => opt.MapFrom(src => GetName(src)))
                .ForMember(dest => dest.NormalContribution, opt => opt.MapFrom(src => GetNormalContribution(src)))
                .ForMember(dest => dest.NormalContributionStartDate, opt => opt.MapFrom(src => GetNormalContributionStartDate(src)))
                .ForMember(dest => dest.EmployerContribution, opt => opt.MapFrom(src => GetEmployerContribution(src)))
                .ForMember(dest => dest.EmployerContributionRate, opt => opt.MapFrom(src => GetEmployerContributionRate(src)))
                .ForMember(dest => dest.TotalContributionRate, opt => opt.MapFrom(src => GetTotalContributionRate(src)))
                .ForMember(dest => dest.ContributionRateFromDate, opt => opt.MapFrom(src => GetContributionRateFromDate(src)))
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => GetEmployeeSalary(src)))
                .ForMember(dest => dest.EmployeeSalaryStartDate, opt => opt.MapFrom(src => GetEmployeeSalaryStartDate(src)))
                .ForMember(dest => dest.TermsOfSalary, opt => opt.MapFrom(src => GetTermsOfSalary(src)))
                .ForMember(dest => dest.EmploymentRate, opt => opt.MapFrom(src => GetEmploymentRate(src)))
                .ForMember(dest => dest.DeviationCode, opt => opt.MapFrom(src => GetDeviationCode(src)))
                .ForMember(dest => dest.DeviationStartDate, opt => opt.MapFrom(src => GetDeviationStartDate(src)))
                .ForMember(dest => dest.DeviationEndDate, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => GetEmployeeSalary(src)))
                .ForMember(dest => dest.EmploymentRateStartDate, opt => opt.MapFrom(src => GetEmploymentRateStartDate(src)))
                //.ForMember(dest => dest.PayGrade, opt => opt.MapFrom(src => GetPaygrade(src)))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.KUND_NR_HOS_MODT))
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => DateTime.Today)) // Fixed by NetsIsParser
                .ForMember(dest => dest.EmploymentTerminationDate, opt => opt.MapFrom(src => GetTerminationDate(src)))
                //.ForMember(dest => dest.ContributorreceivablevoucherUid, opt => opt.MapFrom(src => ConversionHelper.RandomString(17)))
                , loggerFactory);
            return config;
        }

        private static decimal? GetEmployerContributionRate(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.GetDecimal100(record02.ARB_ANDEL_PENS_BIDRAG_PCT);
        }

        private static decimal? GetEmployerContribution(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.GetDecimal100(record02.ARB_ANDEL_BIDRAG_BLB);
        }

        private static decimal? GetNormalContribution(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.GetDecimal100(record02.NORM_BIDRAG_BLB);
        }

        private static DateTime? GetNormalContributionStartDate(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.ParseDate(record02.NORM_BIDRAG_START_DTO);
        }

        private static decimal? GetEmployeeSalary(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.GetDecimal100(record02.PENS_GIV_LOEN_BLB);
        }

        private static DateTime? GetEmployeeSalaryStartDate(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return null;
            }
            return ConversionHelper.ParseDate(record02.PENS_GIV_LOEN_START_DTO);
        }

        private static ReceiptType GetReceiptType(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null || !int.TryParse(record02.PENS_TYPE, out var code))
            {
                return ReceiptType.Payment;
            }
            return code switch
            {
                0 => ReceiptType.Payment,
                1 => ReceiptType.Supplementory,
                2 => ReceiptType.Payment,
                4 => ReceiptType.Volentary,
                10 => ReceiptType.Payment,
                13 => ReceiptType.Payment,
                14 => ReceiptType.Payment,
                15 => ReceiptType.Savings,
                _ => ReceiptType.Payment
            };
        }

        private static string GetPaygrade(InfoRecord00 record)
        {
            var record02 = record.InfoRecord02.FirstOrDefault();
            if (record02 == null)
            {
                return string.Empty;
            }
            return record02.LOEN_TRIN_NR?.Trim() ?? string.Empty;
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

        private static TermsOfSalary GetTermsOfSalary(InfoRecord00 record)
        {
            var record01 = record.InfoRecord01.FirstOrDefault();
            if (record01 == null)
            {
                return TermsOfSalary.Unknown;
            }
            var success = int.TryParse(record01.AFLOEN_FORM, out var code);
            if (!success)
                return TermsOfSalary.Unknown;
            return code switch
            {
                0 => TermsOfSalary.Monthly,
                1 => TermsOfSalary.HourlyByMonth,
                2 => TermsOfSalary.Biweekly,
                3 => TermsOfSalary.Weekly,
                4 => TermsOfSalary.Daily,
                5 => TermsOfSalary.Quarterly,
                _ => TermsOfSalary.Unknown
            };
        }

        private static decimal? GetTotalContributionRate(InfoRecord00 record)
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

        private static DateTime? GetDeviationStartDate(InfoRecord00 record)
        {
            var record03 = record.InfoRecord03.FirstOrDefault();
            if (record03 == null)
            {
                return null;
            }
            return ConversionHelper.ParseDate(record03.AFVIGELSE_DTO);
        }

        private static DeviationCode GetDeviationCode(InfoRecord00 record)
        {
            var record03 = record.InfoRecord03.FirstOrDefault();
            if (record03 == null)
            {
                return DeviationCode.None;
            }
            return DeviationCodeHelper.GetNetsIsDeviationcode(record03.AFVIGELSE_KOD);
        }

        private static decimal? GetEmploymentRate(InfoRecord00 record)
        {
            var record03 = record.InfoRecord03.FirstOrDefault();
            if (record03 == null)
            {
                return null;
            }
            var rate = ConversionHelper.GetDecimal1000(record03.BESK_GRAD_ANT);
            return rate == 0M ? 100M : rate;
        }

        private static DateTime? GetEmploymentRateStartDate(InfoRecord00 record)
        {
            var record03 = record.InfoRecord03.FirstOrDefault();
            if (record03 == null)
            {
                return null;
            }
            return ConversionHelper.ParseDate(record03.BESK_GRAD_START_DTO);
        }

    }
}

