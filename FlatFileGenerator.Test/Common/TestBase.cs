using FlatFileGenerator.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.Test.Common
{
    public abstract class TestBase
    {
        protected FlatFileContext Context { get; } = CreateDbContext();
        protected ILoggerFactory LoggerFactory { get; } = InitializeLoggerFactory();

        private static FlatFileContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlatFileContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), b => b.EnableNullChecks(false))
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return new FlatFileContext(optionsBuilder);
        }

        private static LoggerFactory InitializeLoggerFactory() 
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory;
        }
    }
}
