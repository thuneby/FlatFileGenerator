using FlatFileGenerator.Core.Models.Logs.LogModels;
using FlatFileGenerator.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.DataAccess.Repositories
{
    public class LogModelRepository(FlatFileContext context, ILogger<GuidRepositoryBase<LogModel>> logger) : GuidRepositoryBase<LogModel>(context, logger)
    {
    }
}
