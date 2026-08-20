using AutoMapper;
using FlatFileGenerator.Core.Models.Logs.LogModels;
using FlatFileGenerator.Core.Models.Logs.LogRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers.TextMappers;
using Microsoft.Extensions.Logging;


namespace FlatFileGenerator.FileReader.Business.Mappers.LogMappers
{
    public class LogBaseMapper: TextMapperBase<LogBase, LogModel>
    {
        public LogBaseMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            var config = GetMapperConfiguration(loggerFactory);
            Mapper = config.CreateMapper();
        }

        private static MapperConfiguration GetMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogBase, LogModel>()
                .ForMember(dest => dest.LogDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate10(src.Date)))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp))
                .ForMember(dest => dest.LogType, opt => opt.MapFrom(src => LogType.Info))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => IsSuccessMessage(src.Message)))
                .ForMember(dest => dest.PolicyNumber, opt => opt.MapFrom(src => GetPolicyNumber(src.Message)))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => GetMessagePart(src.Message)))
                .ForMember(dest => dest.DocumentId, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentName, opt => opt.Ignore())
                , loggerFactory
            );
            return config;
        }

        private static string? GetPolicyNumber(string? message)
        {
            if (message == null || message.Length < 15)
                return null;
            int index;
            if (message.StartsWith("Police"))
            {
                index = message.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                return message.Substring(7, index-7).Trim();
            }
            if (message.StartsWith("OiAccountItem"))
            {
                index = message.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                return message.Substring(14, index-14).Trim();
            }
            return null;
        }
        private static bool IsSuccessMessage(string? message)
        {
            
            return message != null && (message.Contains("Betaler ændret", StringComparison.OrdinalIgnoreCase) || message.Contains("Aftale ændret", StringComparison.OrdinalIgnoreCase));

        }

        private static string? GetMessagePart(string? message)
        {
            if (message == null)
                return null;
            if (message.StartsWith("Police") || message.StartsWith("OiAccountItem"))
            {
                var index = message.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                return index > 1? message.Substring(index + 1).Trim() : message;
            }
            return message;
        }
    }
}
