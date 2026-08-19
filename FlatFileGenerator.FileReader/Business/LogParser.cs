using FileHelpers;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Logs.LogModels;
using FlatFileGenerator.Core.Models.Logs.LogRW;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers.LogMappers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FlatFileGenerator.FileReader.Business
{
    public class LogParser
    {
        public Task<IEnumerable<LogModel>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var mapper = new LogBaseMapper();
            var errors = new HashSet<string>();
            var engine = new MultiRecordEngine(typeof(ErrorLog),
                    typeof(InfoLog), typeof(WarningLog), typeof(JavaErrorLog),
                    typeof(ParameterLog), typeof(StatusLog))
            { RecordSelector = LogSelector };
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            var result = engine.ReadStream(new StreamReader(payload, ParserHelperBase.GetEncoding(DocumentType.NetsIs)));
            if (result.Length == 0)
            {
                var exception = new ArgumentException("Fejl - Formatet er ikke en logfil!");
                throw exception;
            }
            if (engine.ErrorManager.HasErrors)
                foreach (var error in engine.ErrorManager.Errors)
                {
                    errors.Add("Fejl i linie: " + error.LineNumber + " - " + error.ExceptionInfo.Message);
                }
            if (errors.Any())
            {
                var exception = new Exception(errors.FirstOrDefault());
                throw exception;
            }


            var endResult = new List<LogModel>();
            var logDate = DateTime.Today;
            var timeStamp = "";

            foreach (var record in result)
            {
                var logType = LogParserHelper.LogTypeDictionary[record.GetType()];
                
                switch (logType)
                {
                    case LogType.Info:
                    case LogType.Error:
                    case LogType.Warning:
                        {
                            var model = mapper.Map((LogBase) record);
                            logDate = model.LogDate; 
                            timeStamp = model.TimeStamp;
                            var message = model.Message?? "";
                            var parameter = message.Contains(" = ") || message.Contains("Parameter");
                            var status = message.Contains("Progress") || message.Contains("batchJob") || message.Contains("Average") || message.Contains("Schedule") || message.Contains("System") || message.Contains("Kørselsplan") || message.Contains("Følgende policer");
                            model.LogType = parameter? LogType.Parameter : status? LogType.Status : logType;
                            endResult.Add(model);
                            break;
                        }
                    case LogType.JavaError:
                        {
                            var javamodel = new LogModel
                            {
                                LogDate = logDate,
                                TimeStamp = timeStamp,
                                Message = ((JavaErrorLog) record).ErrorMessage,
                            };
                            javamodel.LogType = logType;
                            endResult.Add(javamodel);
                            break;
                        }
                    case LogType.Status:
                        {
                            var fulllMessage = ((StatusLog)record).StatusMessage;
                            var isInfoLog = fulllMessage?.Contains("Info:") ?? false; // ToDo find actual date
                            var logModel = new LogModel
                            {
                                LogDate = logDate,
                                TimeStamp = timeStamp,
                                Message = ((StatusLog)record).StatusMessage
                            };
                            logModel.LogType = logType;
                            endResult.Add(logModel);
                            break;
                        }
                    case LogType.Parameter:
                        {
                            var parameterlog = (ParameterLog) record;
                            var parametermodel = new LogModel
                            {
                                LogDate = logDate,
                                TimeStamp = timeStamp,
                                Message = parameterlog.ParameterName + " = " + parameterlog.ParameterValue
                            };
                            parametermodel.LogType = logType;
                            endResult.Add(parametermodel);
                            break;
                        }
                }
                
            }

            return Task.FromResult(endResult.AsEnumerable());
        }


        private static Type LogSelector(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
                return typeof(StatusLog);
            var logType = "";
            if (recordLine.Length >= 32)
                logType = recordLine.Substring(25, 5);
            var progress = recordLine.Contains("Average");
            var parameter = recordLine.Contains(" = ") || recordLine.Contains("Parameter");
            var JavaError = recordLine.Contains(".java") || recordLine.Contains('#');

            return logType switch
            {
                "Info:" => progress? typeof(StatusLog) : typeof(InfoLog),
                "Error" => typeof(ErrorLog),
                "Warn:" => typeof(WarningLog),
                _ => progress ? typeof(StatusLog) :
                     parameter ? typeof(ParameterLog) :
                     JavaError ? typeof(JavaErrorLog) :
                     typeof(StatusLog)
            };
        }

    }
}
