using FlatFileGenerator.Core.Models.Logs.LogModels;
using FlatFileGenerator.Core.Models.Logs.LogRW;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    internal class LogParserHelper: ParserHelperBase
    {
        public static Dictionary<Type, LogType> LogTypeDictionary = new()
        {
            { typeof(InfoLog), LogType.Info },
            {  typeof(ErrorLog), LogType.Error  },
            { typeof(JavaErrorLog), LogType.JavaError },
            { typeof(ParameterLog), LogType.Parameter },
            { typeof(StatusLog), LogType.Status },
            { typeof(WarningLog), LogType.Warning   }
        };

    }
}
