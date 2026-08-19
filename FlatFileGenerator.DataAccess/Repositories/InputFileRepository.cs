using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.DataAccess.Repositories
{
    public class InputFileRepository(FlatFileContext context, ILogger<GuidRepositoryBase<InputFile>> logger) : GuidRepositoryBase<InputFile>(context, logger)
    {
    }
}
